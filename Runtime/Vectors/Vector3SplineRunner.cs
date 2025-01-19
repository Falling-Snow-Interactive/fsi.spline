using UnityEngine;

namespace Fsi.Spline.Vectors
{
    public class Vector3SplineRunner : SplineRunner<Vector3Spline, Vector3Point, Vector3>
    {
        protected override void UpdateRunner()
        {
            transform.position = spline.spline.Evaluate(value).value;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}