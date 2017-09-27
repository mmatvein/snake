namespace Game.Components
{
    using Entitas;
    using System.Collections.Generic;

    [Game]
    public class SnakeVisual : IComponent
    {
        public Definitions.SnakeVisualDefinition snakeVisualDefinition;
        public List<ViewEntity> linkedVisuals;
    }
}