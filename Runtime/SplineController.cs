using UnityEngine;

namespace Fsi.Spline
{
    public class SplineController<TSpline, TCurve, TPoint, TStruct> : MonoBehaviour
        where TSpline : Spline<TCurve, TPoint, TStruct>
        where TCurve : Curve<TPoint, TStruct>
        where TPoint : Point<TStruct>
        where TStruct : struct
    {
        public TSpline spline;

        [Min(1)]
        public int resolution = 10;
        
        [Header("Debugging")]

        [Range(0, 1)]
        [SerializeField]
        private float debugValue = 0.7f;
        public float DebugValue => debugValue;

        protected virtual void OnDrawGizmos()
        {
            spline.DrawSplineGizmos(resolution);
        }
    }
}