namespace Game.Components
{
    using Entitas;
    using System.Collections.Generic;

    [Game]
    public class SnakeVisual : IComponent
    {
        public List<ViewEntity> linkedVisuals;
    }
}