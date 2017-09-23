using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Components
{
    [Game]
    public class SnakePosition : Entitas.IComponent
    {
        public List<Vector2> positions;
    }
}