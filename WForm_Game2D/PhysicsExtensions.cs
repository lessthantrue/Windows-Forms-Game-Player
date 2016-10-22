using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;

namespace WForm_Game2D
{
    /// <summary>
    /// Useful extension methods for bridging the gap between Farseer Physics and XNA
    /// </summary>
    public static class PhysicsExtensions
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
    }
}
