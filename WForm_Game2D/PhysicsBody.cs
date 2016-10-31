using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace WForm_Game2D
{
    /// <summary>
    /// Specifies if the path should be filled or outlined
    /// </summary>
    public enum DrawMode { Fill, Draw }

    /// <summary>
    /// Contains all the information necessary to construct the body when it is added to the world
    /// </summary>
    public struct PhysicsInfo
    {
        public Vector2 Location, Velocity;
        public float Rotation, AngularVelocity;
        public IEnumerable<Shape> Shapes;
    }

    /// <summary>
    /// Describes a physical body in a <see cref="PhysicsScene"/>.
    /// Provides functionality for drawing.
    /// </summary>
    public abstract class PhysicsBody
    {
        /// <summary>
        /// The world that contains the body.
        /// </summary>
        public PhysicsScene OwningScene { get; internal set; }

        /// <summary>
        /// Farseer Physics Body
        /// </summary>
        public Body Body { get; internal set; }

        /// <summary>
        /// Contains all the informaion necessary to construct the body when it is added to the world
        /// </summary>
        protected internal PhysicsInfo Info;

        /// <summary>
        /// Only draws the body if this is true;
        /// </summary>
        public bool Visible { get; set; } = true;
        
        /// <summary>
        /// Color used for <see cref="DrawDynamic"/>
        /// </summary>
        protected virtual Color DrawColor { get; set; }

        /// <summary>
        /// Drawing mode (fill or outline) used for <see cref="DrawDynamic"/>
        /// </summary>
        protected virtual DrawMode Mode { get; set; }

        /// <summary>
        /// Creates a potential Physics Body. To initialize the Farseer Physics body, load this into a world.
        /// </summary>
        /// <param name="info"></param>
        public PhysicsBody(PhysicsInfo info)
        {
            Info = info;
        }

        /// <summary>
        /// Draws the returned image transformed to match the body
        /// </summary>
        public virtual GraphicsPath DrawDynamic() { return null; }
        
        /// <summary>
        /// Use if you want to draw an object manually. Does not transform the image.
        /// </summary>
        public virtual void DrawStatic(Graphics g) { }

        /// <summary>
        /// Deletes the Farseer Physics body, saving all relevant information in <see cref="Info"/>
        /// </summary>
        public void DeconstructFarseerBody()
        {
            PhysicsInfo i;
            i.Shapes = Body.FixtureList.Select(a => a.Shape.ScaledShape(1f / OwningScene.PhysicsScale));
            i.Location = Body.Position / OwningScene.PhysicsScale;
            i.Rotation = Body.Rotation;
            i.Velocity = Body.LinearVelocity / OwningScene.PhysicsScale;
            i.AngularVelocity = Body.AngularVelocity;
        }

        /// <summary>
        /// Called when the Farseer Physics body is constructed. Use this to initialize the Physics
        /// </summary>
        protected internal virtual void FarseerBodyConstruct()
        {
            Body.Position = Info.Location * OwningScene.PhysicsScale;
            Body.LinearVelocity = Info.Velocity * OwningScene.PhysicsScale;
            Body.AngularVelocity = Info.AngularVelocity;
            Body.Rotation = Info.Rotation;
        }

        internal void Draw(Graphics g)
        {
            GraphicsPath p = DrawDynamic();

            if (p == null)
            {
                DrawStatic(g);
                return;
            }

            System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
            m.Scale(OwningScene.DrawingScale, OwningScene.DrawingScale);
            m.Rotate(Body.Rotation);
            m.Translate(Body.Position.X, Body.Position.Y);
            p.Transform(m);

            if (Mode == DrawMode.Draw)
                g.DrawPath(new Pen(DrawColor), p);
            else
                g.FillPath(new SolidBrush(DrawColor), p);
        }
    }
}
