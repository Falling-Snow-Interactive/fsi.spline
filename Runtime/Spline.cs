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
        // public List<TCurve> curves = new();
        public List<TPoint> points = new List<TPoint>();
        
        protected Spline(TPoint start, TPoint end)
        {
        }

        public abstract TPoint Evaluate(float t);
        public abstract TPoint EvaluateCurve(TPoint start, TPoint end, float t);

        public void UpdateJoins()
        {
            // for (int i = 0; i < curves.Count - 1; i++)
            // {
            //     TCurve c0 = curves[i];
            //     TCurve c1 = curves[i + 1];
            //     c1.start = c0.end;
            // }
        }
        
        #if UNITY_EDITOR
        public abstract void DrawSplineGizmos(int resolution);
        public abstract void DrawSplineHandles(SerializedObject serializedObject, int resolution);
        #endif
    }
}
