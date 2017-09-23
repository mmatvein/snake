//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.Components.Timer timer { get { return (Game.Components.Timer)GetComponent(GameComponentsLookup.Timer); } }
    public bool hasTimer { get { return HasComponent(GameComponentsLookup.Timer); } }

    public void AddTimer(float newValue) {
        var index = GameComponentsLookup.Timer;
        var component = CreateComponent<Game.Components.Timer>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimer(float newValue) {
        var index = GameComponentsLookup.Timer;
        var component = CreateComponent<Game.Components.Timer>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimer() {
        RemoveComponent(GameComponentsLookup.Timer);
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTimer;

    public static Entitas.IMatcher<GameEntity> Timer {
        get {
            if (_matcherTimer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Timer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTimer = matcher;
            }

            return _matcherTimer;
        }
    }
}