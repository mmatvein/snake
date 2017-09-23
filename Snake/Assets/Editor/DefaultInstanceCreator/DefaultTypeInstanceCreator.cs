using System;
using Entitas.VisualDebugging.Unity.Editor;

public class DefaultTypeInstanceCreator : IDefaultInstanceCreator {

    public bool HandlesType(Type type) {
        return type == typeof(System.Type);
    }

    public object CreateDefault(Type type) {
        return typeof(UnityEngine.SpriteRenderer);
    }
}
