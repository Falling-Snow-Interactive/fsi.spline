using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Curve : Curve<Vector3Point, Vector3>
    {
        public Vector3Curve(Vector3Point start, Vector3Point end) : base(start, end) { }

        public override Vector3Point Evaluate(float t)
        {
            Vector3 a = Vector3.Lerp(start.value, P0, t);
            Vector3 b = Vector3.Lerp(P0, P1, t);
            Vector3 c = Vector3.Lerp(P1, end.value, t);
            
            Vector3 d = Vector3.Lerp(a, b, t);
            Vector3 e = Vector3.Lerp(b, c, t);
            
            Vector3 f = Vector3.Lerp(d, e, t);
            
            Vector3 tangent = Vector3.Lerp(start.tangentOut, end.tangentIn, t);

            Vector3Point point = new(f, tangent, -tangent);

            return point;
        }
        
        #if UNITY_EDITOR

        public override void DrawCurveGizmos(int resolution)
        {
            start.DrawPointGizmos();
            end.DrawPointGizmos();
            
            List<Vector3Point> points = GetPoints(resolution);
            for (var i = 0; i < points.Count - 1; i++)
            {
                Vector3Point p1 = points[i];
                Vector3Point p2 = points[i + 1];
                Gizmos.DrawLine(p1.value, p2.value);
            }
        }

        public override void DrawCurveHandles(SerializedObject serializedObject, int resolution)
        {
            start.DrawPointHandles(serializedObject);
            end.DrawPointHandles(serializedObject);
            
            Handles.color = Color.grey;
            Handles.DrawDottedLine(P0, P1, 5);
            Handles.color = Color.blue;
            Handles.DrawDottedLine(end.value, start.value, 5);

            Handles.color = Color.white;
            List<Vector3Point> points = GetPoints(resolution);
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3Point p1 = points[i];
                Vector3Point p2 = points[i + 1];
                Handles.DrawLine(p1.value, p2.value, 2f);
            }
        }
        
        #endif
    }
}