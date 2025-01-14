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
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            Slider slider = new Slider
                            {
                                label = "Value",
                                value = 0.7f,
                            };

            slider.RegisterValueChangedCallback(evt =>
                                                {
                                                    float value = slider.value;
                                                });

            return root;
        }
    }
}