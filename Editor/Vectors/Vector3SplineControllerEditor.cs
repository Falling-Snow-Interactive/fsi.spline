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

            foreach (Vector3Curve curve in splineController.spline.curves)
            {
                curve.DrawCurveHandles(serializedObject, splineController.resolution);
            }
            
            Vector3Point valuePoint = splineController.spline.Evaluate(splineController.DebugValue);
            Handles.color = Color.green;
            Handles.FreeMoveHandle(valuePoint.value, 0.5f, Vector3.zero, Handles.SphereHandleCap);
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            return root;
        }
    }
}