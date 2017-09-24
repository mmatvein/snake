namespace Game.Components
{
    [Game]
    public class Ticker : Entitas.IComponent
    {
        public float tickLength;
        public int currentTick;
    }

    [Game]
    public class MainTicker : Entitas.IComponent { }
}
