using System;
using System.Collections.Generic;
using KriptoFX.Realistic_Effects_Pack_3.Effects.Common.Editor;
using UnityEditor;
using UnityEngine;

namespace KriptoFX.Realistic_Effects_Pack_3.Effects.Bloom.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityStandardAssets.CinematicEffects.Bloom))]
    public class BloomEditor : UnityEditor.Editor
    {
        [NonSerialized]
        private List<SerializedProperty> m_Properties = new List<SerializedProperty>();

        BloomGraphDrawer _graph;

        bool CheckHdr(UnityStandardAssets.CinematicEffects.Bloom target)
        {
            var camera = target.GetComponent<Camera>();
            return camera != null && camera.hdr;
        }

        void OnEnable()
        {
            var settings = FieldFinder<UnityStandardAssets.CinematicEffects.Bloom>.GetField(x => x.settings);
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
                var bloom = (UnityStandardAssets.CinematicEffects.Bloom)target;
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
