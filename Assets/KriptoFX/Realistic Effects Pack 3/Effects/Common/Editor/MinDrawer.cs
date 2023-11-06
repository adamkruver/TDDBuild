using UnityEditor;
using UnityEngine;
using MinAttribute = UnityStandardAssets.CinematicEffects.MinAttribute;

namespace KriptoFX.Realistic_Effects_Pack_3.Effects.Common.Editor
{
    [CustomPropertyDrawer(typeof(MinAttribute))]
    internal sealed class MinDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinAttribute attribute = (MinAttribute) base.attribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int v = EditorGUI.IntField(position, label, property.intValue);
                property.intValue = (int)Mathf.Max(v, attribute.min);
            }
            else if (property.propertyType == SerializedPropertyType.Float)
            {
                float v = EditorGUI.FloatField(position, label, property.floatValue);
                property.floatValue = Mathf.Max(v, attribute.min);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Min with float or int.");
            }
        }
    }
}
