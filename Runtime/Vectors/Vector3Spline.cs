using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Spline : Spline<Vector3Point, Vector3>
    {
        public Vector3Spline(Vector3Point start, Vector3Point end) : base(start, end)
        {
            
        }

        public override Vector3Point Evaluate(float t)
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

        public override Vector3Point EvaluateCurve(Vector3Point start, Vector3Point end, float t)
        {
            Vector3 t0 = start.GetP1();
            Vector3 t1 = end.GetP0();
            Vector3 a = Vector3.Lerp(start.value, t0, t);
            Vector3 b = Vector3.Lerp(t0, t1, t);
            Vector3 c = Vector3.Lerp(t0, end.value, t);
            
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            
            Vector3 f = Vector3.Lerp(d, e, t);
            
            Vector3 tangent = Vector3.Lerp(start.tangentOut, end.tangentIn, t);
            
            Vector3Point point = new(f, tangent, -tangent);
            
            return point;
        }

        #if UNITY_EDITOR

        public override void DrawSplineGizmos(int resolution)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3Point point = points[i];
                Vector3Point next = points[i + 1];
                Gizmos.color = Color.white;
                for (float r = 0; r < resolution; r++)
                {
                    Vector3Point p0 = EvaluateCurve(point, next, r/resolution);
                    Vector3Point p1 = EvaluateCurve(point, next, (r + 1)/resolution);
                        
                    Gizmos.DrawLine(p0.value, p1.value);
                }
                
                point.DrawPointGizmos();
            }
        }
        
        public override void DrawSplineHandles(SerializedObject serializedObject, int resolution)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Vector3Point point = points[i];
                if (i < points.Count - 1)
                {
                    Vector3Point next = points[i + 1];
                    Handles.color = Color.cyan;
                    Handles.DrawDottedLine(point.value, next.value, 25f);

                    Handles.color = Color.white;
                    for (float r = 0; r < resolution; r++)
                    {
                        Vector3Point p0 = EvaluateCurve(point, next, r/resolution);
                        Vector3Point p1 = EvaluateCurve(point, next, (r + 1)/resolution);
                        
                        Handles.DrawLine(p0.value, p1.value);
                    }
                }
                
                point.DrawPointHandles(serializedObject);
            }
        }
        
        #endif
    }
}