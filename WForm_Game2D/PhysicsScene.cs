using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.GamePlayer;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;

namespace WForm_Game2D
{
    /// <summary>
    /// Implements 2D physics with Farseer Physics. 
    /// </summary>
    public abstract class PhysicsScene : Scene
    {
        static PhysicsScene()
        {
            FarseerPhysics.Settings.MaxPolygonVertices = 50;
        }

        /// <summary>
        /// Scale that will be applied to all physics objects. 
        /// Default is 0.1. 
        /// Changing this during the game could cause undefined behavior.
        /// </summary>
        public float PhysicsScale { get; protected set; } = 0.1f;

        /// <summary>
        /// Scale that will be applied when drawing the objects. 
        /// Default is 10, to invert the physics scale. 
        /// You can change this during the game.
        /// </summary>
        public float DrawingScale { get; protected set; } = 10f;

        /// <summary>
        /// Farseer Physics World
        /// </summary>
        protected World physWorld;

        /// <summary>
        /// Collection of all <see cref="T"/> in the world
        /// </summary>
        protected HashSet<PhysicsBody> Bodies;
        
        /// <summary>
        /// Where all the objects will be drawn relative to
        /// </summary>
        public Vector2 Camera { get; set; }

        /// <summary>
        /// Called when the scene is first made. World Gravity is default zero.
        /// </summary>
        /// <param name="control"></param>
        public PhysicsScene(GamePlayerControl control) : base(control)
        {
            physWorld = new World(Vector2.Zero);
            Bodies = new HashSet<PhysicsBody>();
        }

        /// <summary>
        /// Called whenever the timer ticks. Steps the physics environment and handles input.
        /// </summary>
        public override void Tick()
        {
            physWorld.Step((float)GameData.actualDt);
            base.Tick();
        }

        /// <summary>
        /// Draws all <see cref="T"/>
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            g.TranslateTransform(Camera.X, Camera.Y);

            foreach (PhysicsBody b in Bodies)
                b.Draw(g);
        }

        /// <summary>
        /// Loads the given <see cref="T"/> into the world
        /// </summary>
        /// <param name="Body"></param>
        public void AddBody(PhysicsBody Body)
        {
            if(Body.OwningScene != null)
            {
                //TODO: make this its own exception type
                throw new Exception("This object is already in a world");
            }

            Body.OwningScene = this;
            Bodies.Add(Body);
            Body.Body = BodyFactory.CreateBody(physWorld);

            foreach(Shape s in Body.Info.Shapes)
            {
                Body.Body.CreateFixture(s);
            }

            Body.FarseerBodyConstruct();
        }

        /// <summary>
        /// Removes the given <see cref="T"/> from the world
        /// </summary>
        /// <param name="Body"></param>
        public void RemoveBody(PhysicsBody Body)
        {
            if (!physWorld.BodyList.Contains(Body.Body))
            {
                //TODO: make this its own exception type
                throw new KeyNotFoundException("This body is not part of this world");
            }

            physWorld.RemoveBody(Body.Body);
            Body.OwningScene = null;
            Bodies.Remove(Body);
        }
    }
}
