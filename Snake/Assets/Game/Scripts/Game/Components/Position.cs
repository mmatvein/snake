using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    using Entitas;

    [Game, View]
    public class Position : IComponent
    {
        public Vector2 value;
    }
}