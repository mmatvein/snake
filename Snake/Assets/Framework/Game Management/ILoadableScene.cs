using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public interface ILoadableScene
    {
        IObservable<Unit> Load();
        void Start();
        IObservable<Unit> Unload();
        Scene GetMainScene();
    }
}