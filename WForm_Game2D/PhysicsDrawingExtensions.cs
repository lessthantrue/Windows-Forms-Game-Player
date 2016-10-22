using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;

namespace WForm_Game2D
{
    /// <summary>
    /// Built-in methods for drawing Farseer Physics bodies with GDI+
    /// </summary>
    public static class PhysicsDrawingExtensions
    {
        /// <summary>
        /// Fills the collision shape
        /// </summary>
        /// <param name="shape">Shape to fill</param>
        /// <param name="g">Graphics object</param>
        /// <param name="transform">shape camera transformation</param>
        /// <param name="b">brush</param>
        public static void FillShape(this Shape shape, Graphics g, FarseerPhysics.Common.Transform transform, Brush b)
        {
            switch (shape.ShapeType)
            {
                case ShapeType.Circle:
                    CircleShape circle = shape as CircleShape;
                    g.FillEllipse(b, 
                        circle.Position.X - circle.Radius, 
                        circle.Position.Y - circle.Radius, 
                        circle.Position.X + circle.Radius, 
                        circle.Position.Y + circle.Radius);
                    break;

                case ShapeType.Chain:
                    ChainShape chain = shape as ChainShape;
                    IEnumerable<Vector2> verts = chain.Vertices;
                    g.DrawLines(
                        new Pen(b), 
                        chain.Vertices.Select((v) => v.Transform(transform)).ToDrawingPoints().ToArray()
                        );
                    break;

                case ShapeType.Edge:
                    EdgeShape edge = shape as EdgeShape;
                    PointF p1 = edge.Vertex1.Transform(transform).ToDrawingPoint();
                    PointF p2 = edge.Vertex2.Transform(transform).ToDrawingPoint();
                    g.DrawLine(new Pen(b), p1, p2);
                    break;

                case ShapeType.Polygon:
                    PolygonShape poly = shape as PolygonShape;
                    g.FillPolygon(b, poly.Vertices.Select((v) => v.Transform(transform)).ToDrawingPoints().ToArray());
                    break;
            }
        }

        /// <summary>
        /// Draws an outline of a collision shape
        /// </summary>
        /// <param name="shape">shape to draw</param>
        /// <param name="g">Graphics object</param>
        /// <param name="transform">shape camera transform</param>
        /// <param name="p">pen</param>
        private static void DrawShape(this Shape shape, Graphics g, FarseerPhysics.Common.Transform transform, Pen p)
        {
            switch (shape.ShapeType)
            {
                case ShapeType.Circle:
                    CircleShape circle = shape as CircleShape;
                    g.DrawEllipse(p,
                        circle.Position.X - circle.Radius,
                        circle.Position.Y - circle.Radius,
                        circle.Position.X + circle.Radius,
                        circle.Position.Y + circle.Radius);
                    break;

                case ShapeType.Chain:
                    ChainShape chain = shape as ChainShape;
                    IEnumerable<Vector2> verts = chain.Vertices;
                    g.DrawLines(
                        p,
                        chain.Vertices.Select((v) => v.Transform(transform)).ToDrawingPoints().ToArray()
                        );
                    break;

                case ShapeType.Edge:
                    EdgeShape edge = shape as EdgeShape;
                    PointF p1 = edge.Vertex1.Transform(transform).ToDrawingPoint();
                    PointF p2 = edge.Vertex2.Transform(transform).ToDrawingPoint();
                    g.DrawLine(p, p1, p2);
                    break;

                case ShapeType.Polygon:
                    PolygonShape poly = shape as PolygonShape;
                    g.DrawPolygon(p, poly.Vertices.Select((v) => v.Transform(transform)).ToDrawingPoints().ToArray());
                    break;
            }
        }

        /// <summary>
        /// Draws all shapes in the body
        /// </summary>
        /// <param name="body">body to draw</param>
        /// <param name="g">Graphics</param>
        /// <param name="p">Pen</param>
        public static void DrawBody(this Body body, Graphics g, Pen p)
        {
            FarseerPhysics.Common.Transform t;
            body.GetTransform(out t);

            foreach(Fixture f in body.FixtureList)
            {
                f.Shape.DrawShape(g, t, p);
            }
        }
        
        /// <summary>
        /// Fills all shapes in the body
        /// </summary>
        /// <param name="body"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void FillBody(this Body body, Graphics g, Brush b)
        {
            FarseerPhysics.Common.Transform t;
            body.GetTransform(out t);

            foreach(Fixture f in body.FixtureList)
            {
                f.Shape.FillShape(g, t, b);
            }
        }
    }
}
