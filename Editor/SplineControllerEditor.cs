using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Spline
{
    public class SplineControllerEditor<TSpline, TPoint, TStruct> : Editor
        where TSpline : Spline<TPoint, TStruct>
        where TPoint : Point<TStruct>
        where TStruct : struct
    {
        SplineController<TSpline, TPoint, TStruct> splineController;
        
        public void OnSceneGUI()
        {
            splineController = (SplineController<TSpline, TPoint, TStruct>)target;
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