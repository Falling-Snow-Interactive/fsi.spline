
using UnityEngine;

namespace fsi.spline
{
    public abstract class Spline<T>
    {
        public T Start { get; set; }
        public T End { get; set; }
        
        public AnimationCurve Curve { get; set; }

        protected Spline(T start, T end)
        {
            Start = start;
            End = end;

            Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }
        
        protected Spline(T start, T end, AnimationCurve curve)
        {
            Start = start;
            End = end;

            Curve = curve;
        }

        protected Spline(T dif)
        {
            Start = default;
            End = dif;
            Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        }

        public abstract T Evaluate(float t);
    }
}
