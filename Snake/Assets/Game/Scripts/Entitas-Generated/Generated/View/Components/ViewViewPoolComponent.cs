//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ViewContext {

    public ViewEntity viewPoolEntity { get { return GetGroup(ViewMatcher.ViewPool).GetSingleEntity(); } }
    public Game.Components.ViewPool viewPool { get { return viewPoolEntity.viewPool; } }
    public bool hasViewPool { get { return viewPoolEntity != null; } }

    public ViewEntity SetViewPool(System.Collections.Generic.List<UnityEngine.GameObject> newFreeViews, System.Collections.Generic.List<UnityEngine.GameObject> newUsedViews) {
        if (hasViewPool) {
            throw new Entitas.EntitasException("Could not set ViewPool!\n" + this + " already has an entity with Game.Components.ViewPool!",
                "You should check if the context already has a viewPoolEntity before setting it or use context.ReplaceViewPool().");
        }
        var entity = CreateEntity();
        entity.AddViewPool(newFreeViews, newUsedViews);
        return entity;
    }

    public void ReplaceViewPool(System.Collections.Generic.List<UnityEngine.GameObject> newFreeViews, System.Collections.Generic.List<UnityEngine.GameObject> newUsedViews) {
        var entity = viewPoolEntity;
        if (entity == null) {
            entity = SetViewPool(newFreeViews, newUsedViews);
        } else {
            entity.ReplaceViewPool(newFreeViews, newUsedViews);
        }
    }

    public void RemoveViewPool() {
        viewPoolEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ViewEntity {

    public Game.Components.ViewPool viewPool { get { return (Game.Components.ViewPool)GetComponent(ViewComponentsLookup.ViewPool); } }
    public bool hasViewPool { get { return HasComponent(ViewComponentsLookup.ViewPool); } }

    public void AddViewPool(System.Collections.Generic.List<UnityEngine.GameObject> newFreeViews, System.Collections.Generic.List<UnityEngine.GameObject> newUsedViews) {
        var index = ViewComponentsLookup.ViewPool;
        var component = CreateComponent<Game.Components.ViewPool>(index);
        component.freeViews = newFreeViews;
        component.usedViews = newUsedViews;
        AddComponent(index, component);
    }

    public void ReplaceViewPool(System.Collections.Generic.List<UnityEngine.GameObject> newFreeViews, System.Collections.Generic.List<UnityEngine.GameObject> newUsedViews) {
        var index = ViewComponentsLookup.ViewPool;
        var component = CreateComponent<Game.Components.ViewPool>(index);
        component.freeViews = newFreeViews;
        component.usedViews = newUsedViews;
        ReplaceComponent(index, component);
    }

    public void RemoveViewPool() {
        RemoveComponent(ViewComponentsLookup.ViewPool);
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

    static Entitas.IMatcher<ViewEntity> _matcherViewPool;

    public static Entitas.IMatcher<ViewEntity> ViewPool {
        get {
            if (_matcherViewPool == null) {
                var matcher = (Entitas.Matcher<ViewEntity>)Entitas.Matcher<ViewEntity>.AllOf(ViewComponentsLookup.ViewPool);
                matcher.componentNames = ViewComponentsLookup.componentNames;
                _matcherViewPool = matcher;
            }

            return _matcherViewPool;
        }
    }
}
