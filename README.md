# Интеграция в редактор Unity для LeoECS
[Unity integration](https://github.com/Leopotam/ecs-unityintegration) for [ECS framework](https://github.com/Leopotam/ecs).

> Проверено на Unity 2020.3 (зависит от Unity) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.

# Содержание
* [Социальные ресурсы](#Социальные-ресурсы)
* [Установка](#Установка)
    * [В виде unity модуля](#В-виде-unity-модуля)
    * [В виде исходников](#В-виде-исходников)
* [Классы](#Классы)
    * [EcsWorldObserver](#EcsWorldObserver)
    * [EcsSystemsObserver](#EcsSystemsObserver)
* [Лицензия](#Лицензия)
* [ЧаВо](#ЧаВо)

# Социальные ресурсы
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)


# Установка

> **ВАЖНО!** Зависит от [LeoECS](https://github.com/Leopotam/ecs) - фреймворк должен быть установлен до этого расширения.

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"com.leopotam.ecs-unityintegration": "https://github.com/Leopotam/ecs-unityintegration.git",
```
По умолчанию используется последняя релизная версия. Если требуется версия "в разработке" с актуальными изменениями - следует переключиться на ветку `develop`:
```
"com.leopotam.ecs-unityintegration": "https://github.com/Leopotam/ecs-unityintegration.git#develop",
```

## В виде исходников
Код так же может быть склонирован или получен в виде архива со страницы релизов.

# Классы

## EcsWorldObserver
`EcsWorldObserver` реализует паттерн "наблюдатель" для отслеживания изменений в мире. Интеграция осуществляется посредством вызова метода `LeopotamGroup.Ecs.UnityIntegration.EcsWorldObserver.Create()` - этот вызов должен быть обернут в `#if UNITY_EDITOR`-блок:
```c#
public class Startup : MonoBehaviour {
    EcsSystems _systems;

    void Start () {
        var world = new EcsWorld ();
        
#if UNITY_EDITOR
        UnityIntegration.EcsWorldObserver.Create (world);
#endif  
        _systems = new EcsSystems(world)
            .Add (new RunSystem1());
            // Остальная инициализация...
        _systems.Init ();
    }
}
```

> **ВАЖНО!** Обозреватель должен быть создан до создания любой сущности в мире.

## EcsSystemsObserver
`EcsSystemsObserver` реализует паттерн "наблюдатель" для отслеживания изменений в системах. Интеграция осуществляется посредством вызова метода `LeopotamGroup.Ecs.UnityIntegration.EcsSystemsObserver.Create()` - этот вызов должен быть обернут в `#if UNITY_EDITOR`-блок:
```c#
public class Startup : MonoBehaviour {
    EcsSystems _systems;

    void Start () {
        var world = new EcsWorld ();
        
#if UNITY_EDITOR
        UnityIntegration.EcsWorldObserver.Create (world);
#endif        
        _systems = new EcsSystems(world)
            .Add (new RunSystem1());
            // Остальная инициализация...
        _systems.Init ();
#if UNITY_EDITOR
        UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
    }
}
```

# Лицензия
Фреймворк выпускается под двумя лицензиями, [подробности тут](./LICENSE.md).

В случаях лицензирования по условиям MIT-Red не стоит расчитывать на
персональные консультации или какие-либо гарантии.

# ЧаВо

### Я не могу редактировать значения полей компонентов в инспекторе. Как я могу это сделать?
Так и задумано, наблюдатель работает с данными только в режиме чтения.

### Я хочу сделать кастомный инспектор для отрисовки моего типа. Как я могу это сделать?
Допустим, у нас есть компонент `MyComponent1`:
```c#
public enum MyEnum { True, False }

public class MyComponent1 {
    public MyEnum State;
    public string Name;
}
```
Инспектор для `MyComponent1` может быть реализован следующим способом (файл должен быть размещен внутри папки `Editor`):
```c#
class MyComponent1Inspector : IEcsComponentInspector {
    Type IEcsComponentInspector.GetFieldType () {
        return typeof (MyComponent1);
    }

    void IEcsComponentInspector.OnGUI (string label, object value, EcsWorld world, ref EcsEntity entityId) {
        var component = value as MyComponent1;
        EditorGUILayout.LabelField (label, EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        EditorGUILayout.EnumPopup ("State", component.State);
        EditorGUILayout.TextField ("Name", component.Name);
        EditorGUI.indentLevel--;
    }
}
```