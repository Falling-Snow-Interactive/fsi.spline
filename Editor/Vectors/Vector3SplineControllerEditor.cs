using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Spline.Vectors
{
    [CustomEditor(typeof(Vector3SplineController))]
    public class Vector3SplineControllerEditor : SplineControllerEditor<Vector3Spline, Vector3Point, Vector3>
    {
        
    }
}