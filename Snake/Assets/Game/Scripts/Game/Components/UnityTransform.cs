using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    using Entitas;

    [View]
    public class UnityTransform : IComponent
    {
        public Transform value;
    }
}