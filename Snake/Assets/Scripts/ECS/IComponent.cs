using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ECS
{
    public interface IComponent { }

    public class Component<Type> : IComponent
    {
        public Type CurrentValue { get { return this.currentValue; } }
        public IObservable<Type> Observable { get { return this.subject.AsObservable(); } }

        Subject<Type> subject;   
        Type currentValue;


        public Component(Type initialValue)
        {
            this.subject = new Subject<Type>();
            this.SetValue(initialValue);
        }

        public void SetValue(Type value)
        {
            this.currentValue = value;
            this.subject.OnNext(value);
        }
    }
}