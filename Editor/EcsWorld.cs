// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.Ecs.UnityIntegration.Editor {
    [CustomEditor (typeof (EcsWorldObserver))]
    sealed class EcsWorldObserverInspector : UnityEditor.Editor {
        public override void OnInspectorGUI () {
            var observer = (EcsWorldObserver) target;
            var stats = observer.GetStats ();
            var guiEnabled = GUI.enabled;
            GUI.enabled = true;
            GUILayout.BeginVertical (GUI.skin.box);
            EditorGUILayout.LabelField ("Components", stats.Components.ToString ());
            EditorGUILayout.LabelField ("Filters", stats.Filters.ToString ());
            EditorGUILayout.LabelField ("Active entities", stats.ActiveEntities.ToString ());
            EditorGUILayout.LabelField ("Reserved entities", stats.ReservedEntities.ToString ());
            GUILayout.EndVertical ();
            GUI.enabled = guiEnabled;
        }
    }
}