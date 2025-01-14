using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    [Serializable]
    public class Vector3Point : Point<Vector3>
    {
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
            Handles.DrawLine(value - tangentIn, value, 2f);
            Vector3 t0 = Handles.PositionHandle(value - tangentIn, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Vector3 tangent = value - t0;
                tangentIn = tangent;
                tangentOut = -tangent;
                serializedObject.ApplyModifiedProperties();
            }
            
            EditorGUI.BeginChangeCheck();
            Handles.color = Color.red;
            Handles.DrawLine(value - tangentOut, value, 2f);
            Vector3 t1 = Handles.PositionHandle(value - tangentOut, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Vector3 tangent = value - t1;
                tangentOut = tangent;
                tangentIn = -tangent;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        #endif
    }
}