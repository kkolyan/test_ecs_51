using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Transform))]
    public class TransformEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            OnInspectorGUI((Transform)target);
        }

        public static void OnInspectorGUI(Transform transform)
        {
            Undo.RecordObject(transform, "Inspector");

            transform.localPosition = EditorGUILayout.Vector3Field("Position", transform.localPosition);
            transform.localScale = EditorGUILayout.Vector3Field("Scale", transform.localScale);

            float meanScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
            float newMeanScale = EditorGUILayout.FloatField("Scale Avg", meanScale);
            if (meanScale != newMeanScale)
            {
                transform.localScale = Vector3.one * newMeanScale;
            }

            transform.localRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", transform.localRotation.eulerAngles));
        }
    }
}