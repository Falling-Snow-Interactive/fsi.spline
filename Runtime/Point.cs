using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.Spline
{
    [Serializable]
    public abstract class Point<T>
        where T : struct
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public T value;
        public T tangentIn;
        public T tangentOut;

        public abstract T GetP0();
        public abstract T GetP1();

        public Point(T value, T tangentIn, T tangentOut)
        {
            this.value = value;
            this.tangentIn = tangentIn;
            this.tangentOut = tangentOut;
        }
        
        #if UNITY_EDITOR
        public abstract void DrawPointGizmos();
        public abstract void DrawPointHandles(SerializedObject serializedObject);
        #endif
    }
}