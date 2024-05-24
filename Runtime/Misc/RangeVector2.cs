namespace EwigeDreamer.Tweening
{
    [System.Serializable]
    internal struct RangeVector2
    {
        public RangeFloat rangeX;
        public RangeFloat rangeY;

        public RangeVector2(RangeFloat rangeX, RangeFloat rangeY)
        {
            this.rangeX = rangeX;
            this.rangeY = rangeY;
        }

        public RangeVector2(float minX, float maxX, float minY, float maxY)
        {
            rangeX = new RangeFloat(minX, maxX);
            rangeY = new RangeFloat(minY, maxY);
        }
    }

#if UNITY_EDITOR
    namespace Editor
    {
        using EwigeDreamer.Extensions.Unity;
        using UnityEngine;
        using UnityEditor;

        [CustomPropertyDrawer(typeof(RangeVector2))]
        internal class RangeVector2Drawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                var xHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rangeX"));
                var yHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rangeY"));
                return xHeight + yHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                float labelWidth = 30f;
                position.GetRows(EditorGUIUtility.standardVerticalSpacing, out var xRangeRect, out var yRangeRect);
                xRangeRect.BiteLeft(labelWidth, 0f, out var xRangeLabelRect, out var xRangeFieldRect);
                yRangeRect.BiteLeft(labelWidth, 0f, out var yRangeLabelRect, out var yRangeFieldRect);
                
                GUI.Box(position, string.Empty, GUI.skin.window);
                EditorGUI.LabelField(xRangeLabelRect, "X:");
                EditorGUI.LabelField(yRangeLabelRect, "Y:");
                EditorGUI.PropertyField(xRangeFieldRect, property.FindPropertyRelative("rangeX"), GUIContent.none);
                EditorGUI.PropertyField(yRangeFieldRect, property.FindPropertyRelative("rangeY"), GUIContent.none);
                
                EditorGUI.indentLevel = indent;
                
                EditorGUI.EndProperty();
            }
        }
    }
#endif
}