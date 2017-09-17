using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Components
{
    public struct SnakePosition
    {
        public readonly List<Vector2> positions;

        public SnakePosition(List<Vector2> positions)
        {
            this.positions = positions;
        }
    }
}