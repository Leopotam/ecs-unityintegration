// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.Ecs.UnityIntegration.Editor {
    [CustomEditor (typeof (EcsFilterObserver))]
    sealed class EcsFilterObserverInspector : UnityEditor.Editor {
        EcsFilterObserver _observer;

        public override void OnInspectorGUI () {
            if (_observer != null) {
                var guiEnabled = GUI.enabled;
                GUI.enabled = true;
                DrawComponents ();
                GUI.enabled = guiEnabled;
                EditorUtility.SetDirty (target);
            }
        }

        void OnEnable () {
            _observer = target as EcsFilterObserver;
        }

        void OnDisable () {
            _observer = null;
        }

        void DrawComponents () {
            GUILayout.BeginVertical (GUI.skin.box);
            var count = _observer.Filter.GetEntitiesCount ();
            EditorGUILayout.LabelField ($"Entities: {count}", EditorStyles.boldLabel);
            if (count > 0) {
                var ego = _observer.World.EntityGameObjects;
                foreach (var idx in _observer.Filter) {
                    ref var entity = ref _observer.Filter.GetEntity(idx);
                    if (entity.IsAlive ()) {
                        ego.TryGetValue (entity.GetInternalId (), out var entityGo);
                        EditorGUILayout.ObjectField (entityGo, typeof (GameObject), true);
                    }
                }
            }
            GUILayout.EndVertical ();
        }
    }
}