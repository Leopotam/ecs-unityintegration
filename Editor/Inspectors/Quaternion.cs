// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2022 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Leopotam.Ecs.UnityIntegration.Editor.Inspectors {
    sealed class QuaternionInspector : IEcsComponentInspector {
        Type IEcsComponentInspector.GetFieldType () {
            return typeof (Quaternion);
        }

        void IEcsComponentInspector.OnGUI (string label, object value, EcsWorld world, ref EcsEntity entityId) {
            EditorGUILayout.Vector3Field (label, ((Quaternion) value).eulerAngles);
        }
    }
}