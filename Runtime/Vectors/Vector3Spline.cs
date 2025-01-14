using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Spline : Spline<Vector3Curve, Vector3Point, Vector3>
    {
        public Vector3Spline(Vector3Point start, Vector3Point end) : base(start, end)
        {
            
        }

        public override Vector3Point Evaluate(float t)
        {
            float tAdj = t * curves.Count;
            int index = (int)tAdj;
            float tCurve = tAdj - index;

            if (index == curves.Count)
            {
                index = curves.Count - 1;
                tCurve = 1;
            }
            
            Vector3Curve curve = curves[index];
            return curve.Evaluate(tCurve);
        }
        
        #if UNITY_EDITOR

        public override void DrawSplineGizmos(int resolution)
        {
            foreach (var curve in curves)
            {
                curve.DrawCurveGizmos(resolution);
            }
        }
        
        public override void DrawSplineHandles(SerializedObject serializedObject, int resolution)
        {
            foreach (var curve in curves)
            {
                curve.DrawCurveHandles(serializedObject, resolution);
            }
        }
        
        #endif
    }
}