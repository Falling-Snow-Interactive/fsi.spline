using System;
using UnityEngine;

namespace Fsi.Spline.Vectors
{
    public class Vector3SplineController : SplineController<Vector3Spline, Vector3Curve, Vector3Point, Vector3>
    {
        private void OnValidate()
        {
            spline.UpdateJoins();
        }

        protected void OnDrawGizmosSelected()
        {
            base.OnDrawGizmos();

            Vector3Point valuePoint = spline.Evaluate(Value);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(valuePoint.value, 0.3f);
        }
    }
}