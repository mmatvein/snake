using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    public struct Input
    {
        public readonly bool up;
        public readonly bool down;
        public readonly bool left;
        public readonly bool right;

        public Input(bool up, bool down, bool left, bool right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
    }
}