using System;
using System.Collections.Generic;
using KriptoFX.Realistic_Effects_Pack_3.Effects.Common.Editor;
using UnityEditor;
using UnityEngine;

namespace KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Downloaded.KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Bloom))]
    public class BloomEditor : UnityEditor.Editor
    {
        [NonSerialized]
        private List<SerializedProperty> m_Properties = new List<SerializedProperty>();

        BloomGraphDrawer _graph;

        bool CheckHdr(Downloaded.KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Bloom target)
        {
            var camera = target.GetComponent<Camera>();
            return camera != null && camera.allowHDR;
        }

        void OnEnable()
        {
            var settings = FieldFinder<Downloaded.KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Bloom>.GetField(x => x.settings);
            foreach (var setting in settings.FieldType.GetFields())
            {
                var prop = settings.Name + "." + setting.Name;
                m_Properties.Add(serializedObject.FindProperty(prop));
            }

            _graph = new BloomGraphDrawer();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!serializedObject.isEditingMultipleObjects)
            {
                EditorGUILayout.Space();
                var bloom = (Downloaded.KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Bloom)target;
                _graph.Prepare(bloom.settings, CheckHdr(bloom));
                _graph.DrawGraph();
                EditorGUILayout.Space();
            }

            foreach (var property in m_Properties)
                EditorGUILayout.PropertyField(property);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
