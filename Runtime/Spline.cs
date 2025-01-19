using System;
using System.Collections.Generic;
using UnityEditor;

namespace Fsi.Spline
{
    [Serializable]
    public abstract class Spline<TPoint, TStruct>
        where TPoint : Point<TStruct>
        where TStruct : struct
    {
        public CurveType curveType = CurveType.Linear;
        public List<TPoint> points = new();
        public bool closed = false;
        
        protected Spline(TPoint start, TPoint end)
        {
        }

        public TPoint Evaluate(float t)
        {
            float tAdj = t * (points.Count - 1);
            int index = (int)tAdj;
            float tCurve = tAdj - index;
            
            if (index == points.Count - 1)
            {
                index = points.Count - 2;
                tCurve = 1;
            }
            
            return EvaluateCurve(points[index], points[index + 1], tCurve);
        }

        public TPoint EvaluateCurve(TPoint start, TPoint end, float t)
        {
            return curveType switch
                   {
                       CurveType.Linear => EvaluateCurveLinear(start, end, t),
                       CurveType.Bezier => EvaluateCurveBezier(start, end, t),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
        
        public abstract TPoint EvaluateCurveLinear(TPoint start, TPoint end, float t);
        public abstract TPoint EvaluateCurveBezier(TPoint start, TPoint end, float t);
        
        #if UNITY_EDITOR
        public abstract void DrawSplineGizmos(int resolution);
        public abstract void DrawSplineHandles(SerializedObject serializedObject, int resolution);
        #endif
    }
}
