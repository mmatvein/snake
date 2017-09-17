using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public struct Velocity
    {
        public readonly Vector2 velocity;

        public Velocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }
    }
}