using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public interface ISystemDeltaTime
    {
        void Update(float dt);
    }
}