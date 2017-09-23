namespace Game.Components
{
    [Input]
    public class Input : Entitas.IComponent
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
}