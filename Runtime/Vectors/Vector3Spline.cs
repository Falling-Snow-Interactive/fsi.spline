using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Spline : Spline<Vector3Point, Vector3>
    {
        public Vector3Spline(Vector3Point start, Vector3Point end) : base(start, end) { }

        public override Vector3Point EvaluateCurveLinear(Vector3Point start, Vector3Point end, float t)
        {
            Vector3 pos = Vector3.Lerp(start.value, end.value, t);
            Vector3 tangent = end.value - start.value;
            Vector3Point point = new(pos, tangent, -tangent);
            return point;
        }

        public override Vector3Point EvaluateCurveBezier(Vector3Point start, Vector3Point end, float t)
        {
            Vector3 p0 = start.value;
            Vector3 p1 = start.value + start.tangentOut;
            Vector3 p2 = end.value + end.tangentIn;
            Vector3 p3 = end.value;
            
            Vector3 a = Vector3.Lerp(p0, p1, t);
            Vector3 b = Vector3.Lerp(p1, p2, t);
            Vector3 c = Vector3.Lerp(p2, p3, t);
            
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

            if (closed)
            {
                Vector3Point point = points[^1];
                Vector3Point next = points[0];
                Gizmos.color = Color.white;
                for (float r = 0; r < resolution; r++)
                {
                    Vector3Point p0 = EvaluateCurve(point, next, r/resolution);
                    Vector3Point p1 = EvaluateCurve(point, next, (r + 1)/resolution);
                        
                    Gizmos.DrawLine(p0.value, p1.value);
                }
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