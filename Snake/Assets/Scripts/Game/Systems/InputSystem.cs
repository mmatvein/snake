using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Systems
{
    using ECS;

    public class InputSystem : ISystemContinuous
    {
        private readonly IEntityDB entityDB;

        public InputSystem(IEntityDB entityDB)
        {
            this.entityDB = entityDB;
        }

        public void Update(float dt)
        {
            this.entityDB.GetEntitiesWithComponent<Component<Components.Input>>()
                .Subscribe(
                    entity =>
                    {
                        this.SetInput(this.entityDB.GetComponent<Component<Components.Input>>(entity));
                    });
        }

        void SetInput(Component<Components.Input> inputComponent)
        {
            bool up = Input.GetKey(KeyCode.UpArrow);
            bool down = Input.GetKey(KeyCode.DownArrow);
            bool left = Input.GetKey(KeyCode.LeftArrow);
            bool right = Input.GetKey(KeyCode.RightArrow);

            inputComponent.SetValue(new Components.Input(up, down, left, right));
        }
    }
}