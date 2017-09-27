//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ViewEntity {

    public Game.Components.UnityTransform unityTransform { get { return (Game.Components.UnityTransform)GetComponent(ViewComponentsLookup.UnityTransform); } }
    public bool hasUnityTransform { get { return HasComponent(ViewComponentsLookup.UnityTransform); } }

    public void AddUnityTransform(UnityEngine.Transform newValue) {
        var index = ViewComponentsLookup.UnityTransform;
        var component = CreateComponent<Game.Components.UnityTransform>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceUnityTransform(UnityEngine.Transform newValue) {
        var index = ViewComponentsLookup.UnityTransform;
        var component = CreateComponent<Game.Components.UnityTransform>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveUnityTransform() {
        RemoveComponent(ViewComponentsLookup.UnityTransform);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class ViewMatcher {

    static Entitas.IMatcher<ViewEntity> _matcherUnityTransform;

    public static Entitas.IMatcher<ViewEntity> UnityTransform {
        get {
            if (_matcherUnityTransform == null) {
                var matcher = (Entitas.Matcher<ViewEntity>)Entitas.Matcher<ViewEntity>.AllOf(ViewComponentsLookup.UnityTransform);
                matcher.componentNames = ViewComponentsLookup.componentNames;
                _matcherUnityTransform = matcher;
            }

            return _matcherUnityTransform;
        }
    }
}