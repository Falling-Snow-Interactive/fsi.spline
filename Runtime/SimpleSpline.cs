using UnityEngine;

namespace fsi.spline
{
    public abstract class SimpleSpline<T> : Spline<T>
    {
        public T Tangent { get; set; }

        public SimpleSpline(T start, T end, T tangent) : base(start, end)
        {
            Tangent = tangent;
        }

        public SimpleSpline(T dif, T tangent) : base(dif)
        {
            Tangent = tangent;
        }
    }

    public class Vector2SimpleSpline : SimpleSpline<Vector2>
    {
        public Vector2SimpleSpline(Vector2 start, Vector2 end, Vector2 tangent) : base(start, end, tangent) { }
        public Vector2SimpleSpline(Vector2 dif, Vector2 tangent) : base(dif, tangent) { }
        
        public override Vector2 Evaluate(float t)
        {
            Vector2 p0 = Start + Tangent;
            Vector2 p1 = End - Tangent;

            Vector2 a = Vector2.Lerp(Start, p0, t);
            Vector2 b = Vector2.Lerp(p0, p1, t);
            Vector2 c = Vector2.Lerp(p1, End, t);
            
            Vector2 d = Vector2.Lerp(a, b, t);
            Vector2 e = Vector2.Lerp(b, c, t);
            
            Vector2 point = Vector2.Lerp(d, e, t);
            return point;
        }
    }
    
    public class Vector3SimpleSpline : SimpleSpline<Vector3>
    {
        public Vector3SimpleSpline(Vector3 start, Vector3 end, Vector3 tangent) : base(start, end, tangent) { }
        
        public override Vector3 Evaluate(float t)
        {
            Vector3 p0 = Start + Tangent;
            Vector3 p1 = End - Tangent;

            Vector3 a = Vector3.Lerp(Start, p0, t);
            Vector3 b = Vector3.Lerp(p0, p1, t);
            Vector3 c = Vector3.Lerp(p1, End, t);
            
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            
            Vector3 point = Vector3.Lerp(d, e, t);
            return point;
        }
    }

    public class ColorSpline : Spline<Color>
    {
        public ColorSpline(Color start, Color end, AnimationCurve curve) : base(start, end, curve) { }

        public override Color Evaluate(float t)
        {
            float tAdj = Curve.Evaluate(t);
            Color lerp = Color.Lerp(Start, End, tAdj);
            return lerp;
        }
    }
}