using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ECS
{
    

    public interface IComponent
    {
    }

    public class Component<Type> : IComponent
    {
        public Type Value { get { return this.currentValue; } }
        Type currentValue;

        public delegate void OnComponentValueUpdatedDelegate(Component<Type> newValue);
        public event OnComponentValueUpdatedDelegate OnValueUpdated;

        public Component(Type initialValue)
        {
            this.SetValue(initialValue);
        }

        public void SetValue(Type value)
        {
            this.currentValue = value;

            if (this.OnValueUpdated != null)
                this.OnValueUpdated(this);
        }
    }
}