using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;

    public class MainTickerCreationSystem : IInitializeSystem
    {
        readonly GameContext gameContext;

        public MainTickerCreationSystem(Contexts contexts)
        {
            this.gameContext = contexts.game;
        }

        public void Initialize()
        {
            GameEntity tickerEntity = this.gameContext.CreateEntity();
            tickerEntity.isMainTicker = true;
            tickerEntity.AddTicker(0.3f, 0);
            tickerEntity.AddTimer(0, 0);
        }
    }
}