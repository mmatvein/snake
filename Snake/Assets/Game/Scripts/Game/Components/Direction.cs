using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public struct SnakeDirection
    {
        public readonly Direction currentDirection;
        public readonly Direction currentWantedDirection;

        public SnakeDirection(Direction currentDirection, Direction currentWantedDirection)
        {
            this.currentDirection = currentDirection;
            this.currentWantedDirection = currentWantedDirection;
        }
    }
}