using ED.Extensions.Unity;
using UnityEditor;
using UnityEngine;

namespace ED.Tweening.Misc
{
    [System.Serializable]
    internal struct RangeVector3
    {
        public RangeFloat rangeX;
        public RangeFloat rangeY;
        public RangeFloat rangeZ;

        public RangeVector3(RangeFloat rangeX, RangeFloat rangeY, RangeFloat rangeZ)
        {
            this.rangeX = rangeX;
            this.rangeY = rangeY;
            this.rangeZ = rangeZ;
        }

        public RangeVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            rangeX = new RangeFloat(minX, maxX);
            rangeY = new RangeFloat(minY, maxY);
            rangeZ = new RangeFloat(minZ, maxZ);
        }
    }

#if UNITY_EDITOR
    namespace Editor
    {
        [CustomPropertyDrawer(typeof(RangeVector3))]
        internal class RangeVector3Drawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                var xHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rangeX"));
                var yHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rangeY"));
                var zHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rangeZ"));
                return xHeight + yHeight + zHeight + EditorGUIUtility.standardVerticalSpacing * 2;
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUI.BeginProperty(position, label, property);
                
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                float labelWidth = 30f;
                position.GetRows(EditorGUIUtility.standardVerticalSpacing, out var xRangeRect, out var yRangeRect, out var zRangeRect);
                xRangeRect.BiteLeft(labelWidth, 0f, out var xRangeLabelRect, out var xRangeFieldRect);
                yRangeRect.BiteLeft(labelWidth, 0f, out var yRangeLabelRect, out var yRangeFieldRect);
                zRangeRect.BiteLeft(labelWidth, 0f, out var zRangeLabelRect, out var zRangeFieldRect);
                
                GUI.Box(position, string.Empty, GUI.skin.window);
                EditorGUI.LabelField(xRangeLabelRect, "X:");
                EditorGUI.LabelField(yRangeLabelRect, "Y:");
                EditorGUI.LabelField(zRangeLabelRect, "Z:");
                EditorGUI.PropertyField(xRangeFieldRect, property.FindPropertyRelative("rangeX"), GUIContent.none);
                EditorGUI.PropertyField(yRangeFieldRect, property.FindPropertyRelative("rangeY"), GUIContent.none);
                EditorGUI.PropertyField(zRangeFieldRect, property.FindPropertyRelative("rangeZ"), GUIContent.none);
                
                EditorGUI.indentLevel = indent;
                
                EditorGUI.EndProperty();
            }
        }
    }
#endif
}