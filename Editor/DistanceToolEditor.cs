using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Sacristan.DistanceTool._Editor
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(Sacristan.DistanceTool.Runtime.DistanceTool))]
    public class DistanceToolEditor : UnityEditor.Editor
    {
        Sacristan.DistanceTool.Runtime.DistanceTool _target;
        GUIStyle style = new GUIStyle();
        public static int count = 0;

        void OnEnable()
        {
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            _target = (Sacristan.DistanceTool.Runtime.DistanceTool)target;

            if (!_target.initialized)
            {
                _target.initialized = true;
                _target.distanceToolName = "Distance Tool " + ++count;
                _target.initialName = _target.distanceToolName;
            }
        }

        public override void OnInspectorGUI()
        {
            if (_target.distanceToolName == "")
            {
                _target.distanceToolName = _target.initialName;
            }

            //UI:
            EditorGUILayout.BeginVertical();

            EditorGUILayout.PrefixLabel("Name");
            _target.distanceToolName = EditorGUILayout.TextField(_target.distanceToolName, GUILayout.ExpandWidth(false));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PrefixLabel("Gizmo Radius");
            _target.gizmoRadius = Mathf.Clamp(EditorGUILayout.Slider(_target.gizmoRadius, 0.1f, 3.0f, GUILayout.ExpandWidth(false)), 0.1f, 100);

            EditorGUILayout.Separator();

            EditorGUILayout.PrefixLabel("Tool Color");
            _target.lineColor = EditorGUILayout.ColorField(_target.lineColor, GUILayout.ExpandWidth(false));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            _target.scaleToPixels = EditorGUILayout.Toggle("Show scale/pixel", _target.scaleToPixels, GUILayout.ExpandWidth(false));

            _target.pixelPerUnit = EditorGUILayout.IntField("Pixels per unit", _target.pixelPerUnit, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();

            _target.showPoints = EditorGUILayout.Foldout(_target.showPoints, "Points");

            if (_target.showPoints)
            {
                _target.startPoint = EditorGUILayout.Vector3Field("start", _target.startPoint);
                _target.endPoint = EditorGUILayout.Vector3Field("end", _target.endPoint);
            }
            EditorGUILayout.EndVertical();

            //update and redraw:
            if (GUI.changed)
            {
                EditorUtility.SetDirty(_target);
            }
        }

        void OnSceneGUI()
        {
            Undo.SetSnapshotTarget(_target, "distance tool undo");
            //lables and handles:
            float distance = Vector3.Distance(_target.startPoint, _target.endPoint);
            float scalePerPixel = distance * _target.pixelPerUnit;

            if (_target.scaleToPixels)
            {
                Handles.Label(_target.endPoint, "       Distance from Start point: " + distance + " - Scale per pixel: " + scalePerPixel + "px", style);
            }
            else
            {
                Handles.Label(_target.endPoint, "        Distance from Start point: " + distance, style);
            }

            //allow adjustment undo:
            _target.startPoint = Handles.PositionHandle(_target.startPoint, Quaternion.identity);
            _target.endPoint = Handles.PositionHandle(_target.endPoint, Quaternion.identity);
        }
    }
}