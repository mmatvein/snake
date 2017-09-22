using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public interface IMainGame
    {
        IObservable<Unit> Load();
        void StartGame();
        Scene GetMainScene();
    }
}