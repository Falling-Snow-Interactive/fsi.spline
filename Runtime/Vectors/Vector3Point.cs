using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Point : Point<Vector3>
    {
        private const float TangentThickness = 0.75f;
        
        public Vector3Point(Vector3 value, Vector3 tangentIn, Vector3 tangentOut) : base(value, tangentIn, tangentOut)
        {
        }
        
        #if UNITY_EDITOR

        public override Vector3 GetP0()
        {
            return value + tangentOut;
        }

        public override Vector3 GetP1()
        {
            return value + tangentIn;
        }

        public override void DrawPointGizmos()
        {
            Gizmos.DrawSphere(value, 0.1f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns>Vector moved.</returns>
        public override void DrawPointHandles(SerializedObject serializedObject)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(value, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                value = newPos;
                serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.green;
            Vector3 p0 = value - tangentIn;
            Handles.DrawLine(p0, value, TangentThickness);
            // Vector3 t0 = Handles.PositionHandle(value - tangentIn, Quaternion.identity);
            Vector3 t0 = Handles.FreeMoveHandle(p0, 0.2f, Vector3.zero, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                switch (tangentType)
                {
                    case TangentType.Flat:
                        break;
                    case TangentType.Mirror:
                        Vector3 tangent = value - t0;
                        tangentIn = tangent;
                        tangentOut = -tangent;
                        break;
                    case TangentType.Split:
                        tangent = value - t0;
                        tangentIn = tangent;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.red;
            Vector3 p1 = value - tangentOut;
            Handles.DrawLine(p1, value, TangentThickness);
            // Vector3 t1 = Handles.PositionHandle(value - tangentOut, Quaternion.identity);
            Vector3 t1 = Handles.FreeMoveHandle(p1, 0.2f, Vector3.zero, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Vector3 tangent;
                switch (tangentType)
                {
                    case TangentType.Flat:
                        break;
                    case TangentType.Mirror:
                        tangent = value - t1;
                        tangentIn = -tangent;
                        tangentOut = tangent;
                        break;
                    case TangentType.Split:
                        tangent = value - t1;
                        tangentOut = tangent;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        #endif
    }
}