namespace ED.Tweening
{
    [System.Serializable]
    internal struct RangeFloat
    {
        public float min;
        public float max;

        public RangeFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

#if UNITY_EDITOR
    namespace Editor
    {
        using ED.Extensions.Unity;
        using UnityEngine;
        using UnityEditor;

        [CustomPropertyDrawer(typeof(RangeFloat))]
        internal class RangeFloatDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                float labelWidth = 30f;
                position.GetColumns(4f, out var minRect, out var maxRect);
                minRect.BiteLeft(labelWidth, 0f, out var minLabelRect, out var minFieldRect);
                maxRect.BiteLeft(labelWidth, 0f, out var maxLabelRect, out var maxFieldRect);
                
                EditorGUI.LabelField(minLabelRect, "Min");
                EditorGUI.LabelField(maxLabelRect, "Max");
                EditorGUI.PropertyField(minFieldRect, property.FindPropertyRelative("min"), GUIContent.none);
                EditorGUI.PropertyField(maxFieldRect, property.FindPropertyRelative("max"), GUIContent.none);
                
                EditorGUI.indentLevel = indent;
                
                EditorGUI.EndProperty();
            }
        }
    }
#endif
}