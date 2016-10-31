using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;

namespace WForm_Game2D
{
    /// <summary>
    /// Useful extension methods for various operations
    /// </summary>
    public static class Game2DExtensions
    {
        /// <summary>
        /// Transforms a Vector
        /// </summary>
        /// <param name="v"></param>
        /// <param name="t">Farseer Transform</param>
        /// <returns></returns>
        public static Vector2 Transform(this Vector2 v, Transform t)
        {
            return Vector2.Transform(v, Matrix.CreateRotationZ(t.q.GetAngle()) * Matrix.CreateTranslation(new Vector3(v.X, v.Y, 0)));
        }

        /// <summary>
        /// Returns a shape scaled by a factor
        /// </summary>
        /// <param name="shape">Shape to scale</param>
        /// <param name="scale">Scalar</param>
        /// <returns>Scaled Shape</returns>
        public static Shape ScaledShape(this Shape shape, float scale)
        {
            switch (shape.ShapeType)
            {
                case ShapeType.Circle:
                    CircleShape circle = shape as CircleShape;
                    return new CircleShape(circle.Radius * scale, shape.Density);

                case ShapeType.Chain:
                    ChainShape chain = shape as ChainShape;
                    return new ChainShape(new Vertices(chain.Vertices.Select(a => a * scale)));

                case ShapeType.Edge:
                    EdgeShape edge = shape as EdgeShape;
                    return new EdgeShape(edge.Vertex1 * scale, edge.Vertex2 * scale);

                case ShapeType.Polygon:
                    PolygonShape poly = shape as PolygonShape;
                    return new PolygonShape(new Vertices(poly.Vertices.Select(a => a * scale)), shape.Density);

                case ShapeType.Unknown:
                    throw new ArgumentException("Could not determine shape type");

                default:
                    return null;
            }
        }
    }
}
