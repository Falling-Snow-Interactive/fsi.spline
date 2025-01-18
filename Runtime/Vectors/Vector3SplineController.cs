using UnityEngine;

namespace Fsi.Spline.Vectors
{
    public class Vector3SplineController : SplineController<Vector3Spline, Vector3Curve, Vector3Point, Vector3>
    {
        private void OnValidate()
        {
            spline.UpdateJoins();
        }
    }
}