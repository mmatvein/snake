namespace Game.Components
{
    public enum Direction : int
    {
        Undefined,
        Up,
        Down,
        Left,
        Right
    }

    [Game]
    public class SnakeDirection : Entitas.IComponent
    {
        public Direction currentDirection;
        public Direction currentWantedDirection;
    }
}