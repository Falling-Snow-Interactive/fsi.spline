using System;
using UnityEngine;

namespace Fsi.Spline
{
    [Serializable]
    public abstract class SplineRunner<TSpline, TPoint, TStruct> : MonoBehaviour
        where TSpline : Spline<TPoint, TStruct>
        where TPoint : Point<TStruct>
        where TStruct : struct

    {
        [Range(0,1)]
        public float value = 0;
        public float speed = 0.2f;
        
        public SplineController<TSpline, TPoint, TStruct> spline;

        protected void Update()
        {
            value += Time.deltaTime * speed;
            value %= 1f;

            UpdateRunner();
        }

        private void OnValidate()
        {
            UpdateRunner();
        }

        protected abstract void UpdateRunner();
    }
}