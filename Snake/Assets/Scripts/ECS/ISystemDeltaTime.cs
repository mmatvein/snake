﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public interface ISystemContinuous
    {
        void Update(float dt);
    }
}