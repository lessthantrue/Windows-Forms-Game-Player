using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace WForm_Game2D
{
    /// <summary>
    /// Some extension functions to convert Farseer physics to XNA
    /// </summary>
    internal static class Conversions
    {
        /// <summary>
        /// Converts XNA to GDI+
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static PointF ToDrawingPoint(this Vector2 vec)
        {
            return new PointF(vec.X, vec.Y);
        }

        /// <summary>
        /// List mapped <see cref="ToDrawingPoint(Vector2)"/>
        /// </summary>
        /// <param name="verts"></param>
        /// <returns></returns>
        public static IEnumerable<PointF> ToDrawingPoints(this IEnumerable<Vector2> verts)
        {
            return verts.Select(a => a.ToDrawingPoint());
        }
    }
}
