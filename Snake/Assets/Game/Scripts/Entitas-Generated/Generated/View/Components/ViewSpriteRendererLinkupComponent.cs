//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class ViewEntity {

    public SpriteRendererLinkupComponent spriteRendererLinkup { get { return (SpriteRendererLinkupComponent)GetComponent(ViewComponentsLookup.SpriteRendererLinkup); } }
    public bool hasSpriteRendererLinkup { get { return HasComponent(ViewComponentsLookup.SpriteRendererLinkup); } }

    public void AddSpriteRendererLinkup(Game.Components.SpriteRendererLinkup newValue) {
        var index = ViewComponentsLookup.SpriteRendererLinkup;
        var component = CreateComponent<SpriteRendererLinkupComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceSpriteRendererLinkup(Game.Components.SpriteRendererLinkup newValue) {
        var index = ViewComponentsLookup.SpriteRendererLinkup;
        var component = CreateComponent<SpriteRendererLinkupComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSpriteRendererLinkup() {
        RemoveComponent(ViewComponentsLookup.SpriteRendererLinkup);
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

    static Entitas.IMatcher<ViewEntity> _matcherSpriteRendererLinkup;

    public static Entitas.IMatcher<ViewEntity> SpriteRendererLinkup {
        get {
            if (_matcherSpriteRendererLinkup == null) {
                var matcher = (Entitas.Matcher<ViewEntity>)Entitas.Matcher<ViewEntity>.AllOf(ViewComponentsLookup.SpriteRendererLinkup);
                matcher.componentNames = ViewComponentsLookup.componentNames;
                _matcherSpriteRendererLinkup = matcher;
            }

            return _matcherSpriteRendererLinkup;
        }
    }
}
