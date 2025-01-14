using System;
using System.Collections.Generic;
using UnityEditor;

namespace Fsi.Spline
{
    [Serializable]
    public abstract class Curve<TPoint, TStruct>
        where TPoint : Point<TStruct>
        where TStruct : struct
    {
        public TPoint start;
        public TPoint end;
        public TStruct P0 => start.GetP0();
        public TStruct P1 => end.GetP1();
        
        protected Curve(TPoint start, TPoint end)
        {
            this.start = start;
            this.end = end;
        }

        public abstract TPoint Evaluate(float t);

        public List<TPoint> GetPoints(int resolution)
        {
            List<TPoint> points = new();

            for (int i = 0; i <= resolution; i++)
            {
                points.Add(Evaluate((float)i/resolution));
            }
            
            return points;
        }
        
        #if UNITY_EDITOR
        public abstract void DrawCurveGizmos(int resolution);
        public abstract void DrawCurveHandles(SerializedObject serializedObject, int resolution);
        #endif
    }
}