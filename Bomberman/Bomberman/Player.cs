using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Runtime.Serialization;
using Bomberman.GameSaving;
namespace Bomberman
{
   
    public class Effect {
        const int TIME_STEP = 1000;
        Modifier mod;
        int time;
        public bool Active {
            get { return ( time > 0 );}
        }
        public Effect(Modifier m) {
            mod = m;
            time = m.getRespirationTime();
        }
        public void onUpdate(Player p) {
            time -= TIME_STEP;
            if (time > 0) {
                mod.onUpdate(p);
            }
        }
        public void onBegin(Player p) {
            Debug.WriteLine("begin effect");
            mod.onBegin(p);
        }
        public void onEnd(Player p) {
            mod.onEnd(p);
        }
        public void Draw(int x, int y, SpriteBatch spriteBatch, ContentManager contentManager ) {
            if (mod.getRespirationTime() > 0) {
                MazeBlock block = mod.getBlock();
                block.Draw((uint)x, (uint)y, spriteBatch, contentManager);
            }
           
        }
        public Effect(Modifier m, int _time)
        {
            mod = m;
            time = _time;
        }

        public EffectDO convertToEffectDO(IModiferSerializer serilaizer)
        {
            return new EffectDO(serilaizer.Serialize(mod), time);
        }

    }
    [DataContract()]
    public class Player : GameObject
    {
        #region CONSTS

         const int NONE_DIRECTION = 0;
         const int INTERVAL_ACTION = 500;
         const int INITIAL_BOMBS_AVAILABLE = 1;
         const int INITIAL_EXPLOSION_RANGE = 1;
         const int UP = 1;
         const int RIGHT = 2;
         const int DOWN = 3;
        const int LEFT = 4;
        public  const int MODE_MOVEMENT_DEFAULT = 0;
        public  const int MODE_MOVEMENT_THROW = 1;

        #endregion
        #region FIELDS
        private List<Effect> effects= new List<Effect> ();
        private Texture2D texture;
        private Point position;
       
        float speed = 0;
        int interval = 0;
        private int direction;
        int bombsAvailable = INITIAL_BOMBS_AVAILABLE;
        int explosionRange = INITIAL_EXPLOSION_RANGE;
        private bool alive = true;
        int movementMode = MODE_MOVEMENT_DEFAULT;
        bool touched = false;
        
        #endregion
        #region ATTRIBUTES
      
        [IgnoreDataMember()]
        public List<Effect> Effects {
            get { return effects;  }
        }
        [DataMember()]
        public int BombsAvailable {
            get { //Debug.WriteLine("Bombs avaiable"); 
                return bombsAvailable; }
            set { bombsAvailable = value; }
        }
        [DataMember()]
        public bool Touched
        {
            get { //Debug.WriteLine("Touched"); 
                return touched; }
            set { touched = value; }
        }
        [DataMember()]
        public bool Alive
        {
            get { return alive; }
            set {
                if (!value) {
                    stop();
                }
                alive = value;
            }
        }
       [DataMember()]
        public int ExplosionRange {
            get { //Debug.WriteLine("Exploding Range"); 
                return explosionRange; }
            set {
                //TODO Bomb.setRange()?
                explosionRange = value; 
            }
        }
        [DataMember()]
        public int Direction
        {
            get { //Debug.WriteLine("Direction"); 
                return direction; }
            set { direction = value; }
        }
        [DataMember()]
        public int MovementMode {
            get { //Debug.WriteLine("MovementMode"); 
                return movementMode; }
            set { this.movementMode = value; }
        }
        [DataMember()]
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0.001)
                {
                    speed = 1;
                }
                else
                {
                    speed = value;
                }
            }
        }

        private Point? previousPosition = null;
        [DataMember()]
        public Point PreviousPosition
        {
            get
            {
                if (previousPosition == null)
                    return position;
                else
                    return (Point)previousPosition;
            }
            set { previousPosition = value; }
        }

        [DataMember()]
        public Point Position
        {
            set
            {
                position = value;
            }
            get { return position; }
        }
        #endregion
        #region SERIALIZATION
        [DataMember()]
        public List<EffectDO> effectsDo
        {
            get
            {
                IModiferSerializer modSerializer = new ReflectionModifierSerializer();
                List<EffectDO> tmpList = new List<EffectDO>();
                foreach (Effect effect in effects)
                {
                    tmpList.Add(effect.convertToEffectDO(modSerializer));
                }
                return tmpList;
            }
            set{
                IModiferSerializer modSerializer = new ReflectionModifierSerializer();
                effects = new List<Effect>();
                foreach (EffectDO data in value)
                {
                    effects.Add(data.createEffect(modSerializer));
                }
            }
        }

        public Boolean isThisOne(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                Player _player = (Player)gameObject;
                return position.Equals(_player.position);
            }
            return false;
        }

        #endregion
        #region INITIALIZATION
        public Player()
        {
            effects = new List<Effect>();
            FindBeginPosition();
            Speed = 1;
            direction = NONE_DIRECTION;
        }

        public void LoadGraphic(SpriteBatch spriteBatch , ContentManager conentManager)
        {
           /* Debug.WriteLine(this.GetType().ToString() + " : " + Maze.BlockWidth + " " + Maze.BlockHeight);
            Debug.WriteLine(this.GetType().ToString() + " : " + (int)Maze.BlockWidth + " " + (int)Maze.BlockHeight);*/
            texture = conentManager.Load<Texture2D>("Chests\\bombWithEyes2");
        }


        #endregion
        #region DRAW
        public override void Draw(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            var p0 = StdGameScaler.Instance.Transform(PreviousPosition);
            var p1 = StdGameScaler.Instance.Transform(position);

            var p = p0 + (p1 - p0) * interval*Speed / INTERVAL_ACTION;

            if (texture == null || texture.GraphicsDevice != spriteBatch.GraphicsDevice)
                LoadGraphic(spriteBatch, contentManager);

            spriteBatch.Draw(texture, StdGameScaler.Instance.GetRectangle(p), Color.White);
        }



        #endregion
        #region HELPERS
        private void countEmptyNeighborhood(int x, int y, ref List<Point> visited)
        {

            if (Engine.Instance.Maze.Block[(uint)x, (uint)y] is Empty)
            {

                visited.Add(new Point(x, y));
                Point p = new Point(x + 1, y);
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x + 1, y, ref visited);
                }
                p.X = x - 1;
                p.Y = y;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x - 1, y, ref visited);
                }
                p.X = x;
                p.Y = y + 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y + 1, ref visited);
                }
                p.X = x;
                p.Y = y - 1;
                if (!visited.Contains(p))
                {
                    countEmptyNeighborhood(x, y - 1, ref visited);
                }


            }


        }
        public void FindBeginPosition()
        {
            //int empties = 0;
            for (uint i = 0; i < Maze.Width; i++)
            {
                for (uint j = 0; j < Maze.Height; j++)
                {
                    if (Engine.Instance.Maze.Block[i, j] is Empty)
                    {
                        List<Point> visited = new List<Point>();
                        countEmptyNeighborhood((int)i, (int)j, ref visited);
                        // Debug.WriteLine( visited.Count().ToString());
                        if (visited.Count() > 2)
                        {
                            SetPosition(i, j);


                            goto Found;
                        }
                        visited.Clear();
                    }
                }
            }
        Found: { }
        }

        public void UpdatePosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public void SetPosition(int x, int y)
        {
            Position = new Point(x, y);
        }
        public void SetPosition(uint x, uint y)
        {
            Position = new Point((int)x, (int)y);
        }
        protected Rectangle ComputePosition(int x, int y)
        {
            return new Rectangle((int)x * (int)Maze.BlockWidth, (int)y * (int)Maze.BlockHeight, (int)Maze.BlockWidth, (int)Maze.BlockHeight);
        }

        #endregion
        #region LOGIC
        public void addModifier(Modifier m) {
            Debug.WriteLine("add modifier");
            m.apply(this);
            Effect effect = new Effect( m );
            this.effects.Add( effect );
            effect.onBegin(this);
            Sound.Instance.Play("Spup");
        }
        
        public void setBomb() {
            int count = 0;

            count = Engine.Instance.Bombs.Count(b => (b.isActive() && b.playered));

            if (count < BombsAvailable)
            {
                Debug.WriteLine("setBomb " + Position.X + " " + Position.Y + " " + explosionRange);
                Engine.Instance.AddObject(new Bomb(Position.X, Position.Y, explosionRange, true));
            }

           
        }


        public void goInDirection(int dir)
        {

            switch (dir)
            {
                case UP:
                    {
                        if (Engine.Instance.Maze.Block[(uint)Position.X, (uint)(Position.Y + 1)] is Empty)
                        {
                            bool blocked = false;
                            foreach (var bomb in Engine.Instance.Bombs )
                            {
                               
                                if ( (bomb.Position.X == Position.X && bomb.Position.Y == Position.Y + 1))
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                            if(!blocked)
                                UpdatePosition(Position.X, Position.Y + 1);
                        }

                        break;
                    }
                case RIGHT:
                    {
                        if (Engine.Instance.Maze.Block[(uint)Position.X + 1, (uint)(Position.Y)] is Empty)
                        {
                            bool blocked = false;
                            foreach (var bomb in Engine.Instance.Bombs)
                            {
                                if (bomb.Position.X == Position.X + 1 && bomb.Position.Y == Position.Y)
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                            if (!blocked)
                                UpdatePosition(Position.X + 1, Position.Y);
                        }
                        break;
                    }
                case DOWN:
                    {
                        if (Engine.Instance.Maze.Block[(uint)Position.X, (uint)(Position.Y - 1)] is Empty)
                        {
                            bool blocked = false;
                            foreach (var bomb in Engine.Instance.Bombs)
                            {
                                if (bomb.Position.X == Position.X && bomb.Position.Y == Position.Y - 1)
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                            if (!blocked)
                                UpdatePosition(Position.X, Position.Y - 1);
                        }
                        break;
                    }
                case LEFT:
                    {
                        if (Engine.Instance.Maze.Block[(uint)Position.X - 1, (uint)(Position.Y)] is Empty)
                        {
                            bool blocked = false;
                            foreach (var bomb in Engine.Instance.Bombs)
                            {
                                if (bomb.Position.X == Position.X - 1 && bomb.Position.Y == Position.Y)
                                {
                                    blocked = true;
                                    break;
                                }
                            }
                            if (!blocked)
                                UpdatePosition(Position.X - 1, Position.Y);
                        }
                        break;
                    }
            }

        }
        public void move(Vector2 delta)
        {
            //Debug.WriteLine("In da move");
            float absX = delta.X < 0 ? -delta.X : delta.X;
            float absY = delta.Y < 0 ? -delta.Y : delta.Y;
            float range = 1;
            Touched = true;
            if (absX > absY)
            {
                if (delta.X < 0)
                {
                    if (Engine.Instance.Maze.Block[(uint)Position.X - 1, (uint)(Position.Y)] is Empty)
                    {
                        direction = LEFT;
                        range = absX/Maze.BlockWidth;
                    }
                        
                }
                if (delta.X > 0)
                {
                    if (Engine.Instance.Maze.Block[(uint)Position.X + 1, (uint)(Position.Y)] is Empty)
                    {
                        direction = RIGHT;
                        range = absX/Maze.BlockWidth;
                    }
                }
            }
            else
            {
                if (delta.Y > 0)
                {
                    if (Engine.Instance.Maze.Block[(uint)Position.X, (uint)(Position.Y + 1)] is Empty)
                    {
                        range = absY/Maze.BlockHeight;
                        direction = UP;
                    }
                }
                if (delta.Y < 0)
                {
                    if (Engine.Instance.Maze.Block[(uint)Position.X, (uint)(Position.Y - 1)] is Empty)
                    {
                        direction = DOWN;
                        range = absY/Maze.BlockHeight;
                    }
                }
            }
            if (MovementMode == MODE_MOVEMENT_THROW) {
                Speed = range;
            }
            //Debug.WriteLine(direction.ToString() + " " + range.ToString());
            //interval =( INTERVAL_ACTION / Speed ) + 1;
        }
        public void stop()
        {
            Direction = NONE_DIRECTION;
        }
        public override void Update(GameTime gameTime)
        {
            interval += gameTime.ElapsedGameTime.Milliseconds;

            if (interval > INTERVAL_ACTION / Speed && Alive)
            {
                PreviousPosition = position;
                interval = 0;
                for (int i = 0; i < effects.Count(); i++ )
                {
                    if (effects[i].Active)
                    {
                        effects[i].onUpdate(this);
                    }
                    else
                    {
                        effects[i].onEnd(this);
                        effects.RemoveAt(i);
                    }
                }
                if (MovementMode == MODE_MOVEMENT_THROW) {
                    Speed /= 4;
                }
                foreach (var explosion in Engine.Instance.Maze.Explosions)
                {
                    if (explosion.X == Position.X && explosion.Y == Position.Y) {
                        Alive = false;
                    }
                }
                if(Alive)
                    goInDirection(Direction);
                foreach (var explosion in Engine.Instance.Maze.Explosions)
                {
                    if (explosion.X == Position.X && explosion.Y == Position.Y)
                    {
                        Alive = false;
                    }
                }
                Engine.Instance.Maze.clearExplosions();
                if (Engine.Instance.Maze.Modifier[(uint)Position.X, (uint)Position.Y] != null)
                {
                    Modifier m = Engine.Instance.Maze.Modifier[(uint)Position.X, (uint)Position.Y];
                    Engine.Instance.Maze.destroyModifier((uint)Position.X, (uint)Position.Y);
                    addModifier( m );
                }
                Touched = false;
            }



            x = position.X * (int)Maze.BlockWidth;
            y = position.Y * (int)Maze.BlockHeight;

        }
        #endregion

    }
}
