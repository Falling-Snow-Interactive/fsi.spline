
using UnityEngine;

namespace fsi.spline
{
    public abstract class Spline<T>
    {
        public T Start { get; set; }
        public T End { get; set; }

        protected Spline(T start, T end)
        {
            Start = start;
            End = end;
        }

        public abstract T Evaluate(float t);
    }
}
