using System.Collections.Generic;
using System.Linq;
using Sources.Attributes;
using Sources.Extensions.Types;
using UnityEditor;
using UnityEngine;

namespace Editor.TypedPopup
{
    [CustomPropertyDrawer(typeof(TypedPopupAttribute))]
    public class TypedPopupDrawer : PropertyDrawer
    {
        private int _selectedIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object @object = property.serializedObject.targetObject;
            
            TypedPopupAttribute typedPopupAttribute = attribute as TypedPopupAttribute;

            List<string> types = typedPopupAttribute.Type.GetAllImplementations().Select(type => type.Name).ToList();

            _selectedIndex = types.IndexOf(types.Find(type => type == property.stringValue) ?? types[0]);

            int selectedIndex = EditorGUI.Popup(position, "Type", _selectedIndex, types.ToArray());
            property.stringValue = types[selectedIndex];
            
            if(_selectedIndex == selectedIndex)
                return;

            _selectedIndex = selectedIndex;
            
            fieldInfo.SetValue(@object, types[selectedIndex]);

            if (@object is ScriptableObject scriptableObject)
            {
                EditorUtility.SetDirty(scriptableObject);
                AssetDatabase.SaveAssets();
            }
        }
    }
}