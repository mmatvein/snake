//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Game.Components.Ticker gameComponentsTicker { get { return (Game.Components.Ticker)GetComponent(GameComponentsLookup.GameComponentsTicker); } }
    public bool hasGameComponentsTicker { get { return HasComponent(GameComponentsLookup.GameComponentsTicker); } }

    public void AddGameComponentsTicker(float newTickLength, int newCurrentTick) {
        var index = GameComponentsLookup.GameComponentsTicker;
        var component = CreateComponent<Game.Components.Ticker>(index);
        component.tickLength = newTickLength;
        component.currentTick = newCurrentTick;
        AddComponent(index, component);
    }

    public void ReplaceGameComponentsTicker(float newTickLength, int newCurrentTick) {
        var index = GameComponentsLookup.GameComponentsTicker;
        var component = CreateComponent<Game.Components.Ticker>(index);
        component.tickLength = newTickLength;
        component.currentTick = newCurrentTick;
        ReplaceComponent(index, component);
    }

    public void RemoveGameComponentsTicker() {
        RemoveComponent(GameComponentsLookup.GameComponentsTicker);
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

    static Entitas.IMatcher<GameEntity> _matcherGameComponentsTicker;

    public static Entitas.IMatcher<GameEntity> GameComponentsTicker {
        get {
            if (_matcherGameComponentsTicker == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GameComponentsTicker);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGameComponentsTicker = matcher;
            }

            return _matcherGameComponentsTicker;
        }
    }
}