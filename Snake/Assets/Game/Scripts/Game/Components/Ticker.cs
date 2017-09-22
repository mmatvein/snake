namespace Game.Components
{
    public struct Ticker
    {
        public readonly float tickLength;
        public readonly int currentTick;

        public Ticker(float tickLength, int currentTick)
        {
            this.tickLength = tickLength;
            this.currentTick = currentTick;
        }
    }
}
