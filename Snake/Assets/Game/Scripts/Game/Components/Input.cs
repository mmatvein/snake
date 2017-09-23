namespace Game.Components
{
    using Entitas.CodeGeneration.Attributes;
    [Input, Unique]
    public class Input : Entitas.IComponent
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
}