using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    public class GameSystems : Feature
    {
        public GameSystems(Contexts contexts) : base("Game Systems")
        {
            this.Add(new MainTickerCreationSystem(contexts));
            this.Add(new SnakeCreationSystem(contexts));

            this.Add(new TimerSystem(contexts, () => Time.deltaTime));
            this.Add(new TickerSystem(contexts));

            this.Add(new SnakeMoveSystem(contexts));
            this.Add(new SnakeVisualCreationSystem(contexts));
        }
    }
}