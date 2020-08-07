using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Barrier editor tool", typeof(Barrier))]
public class BarrierEditorTool : EditorTool
{
    public override GUIContent toolbarIcon => EditorGUIUtility.IconContent("CustomTool@2x");

    public override void OnToolGUI(EditorWindow window)
    {
        var barrier = target as Barrier;
        if (!barrier)
        {
            return;
        }
        var targetTransform = barrier.transform;
        EditorGUI.BeginChangeCheck();

        var newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            var distance = barrier.pathCreator.path.GetClosestDistanceAlongPath(newPosition);
            barrier.transform.position = barrier.pathCreator.path.GetPointAtDistance(distance);
            barrier.transform.rotation = barrier.pathCreator.path.GetRotationAtDistance(distance);
        }
    }
}
