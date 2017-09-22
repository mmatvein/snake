using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Visuals
{
    public struct SnakeVisuals
    {
        public readonly List<GameObject> snakeBlocks;

        public SnakeVisuals(List<GameObject> snakeBlocks)
        {
            this.snakeBlocks = snakeBlocks;
        }
    }
}