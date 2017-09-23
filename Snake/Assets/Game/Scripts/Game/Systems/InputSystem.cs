using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;

    public class InputSystem : IInitializeSystem, IExecuteSystem
    {
        readonly InputContext inputContext;
        InputEntity inputEntity;

        public InputSystem(Contexts contexts)
        {
            this.inputContext = contexts.input;
        }

        public void Initialize()
        {
            this.inputEntity = this.inputContext.SetInput(false, false, false, false);
        }

        public void Execute()
        {
            this.inputEntity.ReplaceInput(Input.GetKey(KeyCode.UpArrow), Input.GetKey(KeyCode.DownArrow), Input.GetKey(KeyCode.LeftArrow), Input.GetKey(KeyCode.RightArrow));
        }
    }
}