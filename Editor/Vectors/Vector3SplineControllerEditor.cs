using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Spline.Vectors
{
    [CustomEditor(typeof(Vector3SplineController))]
    public class Vector3SplineControllerEditor : Editor
    {
        Vector3SplineController splineController;
        
        public void OnSceneGUI()
        {
            splineController = (Vector3SplineController)target;
            splineController.spline.DrawSplineHandles(serializedObject, splineController.resolution);
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            return root;
        }
    }
}