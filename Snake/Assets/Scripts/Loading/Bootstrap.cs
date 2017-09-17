using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public static class Bootstrap
    {
        public static readonly SceneManager SceneManager;

        static Bootstrap()
        {
            Bootstrap.SceneManager = new SceneManager();
        }

        public static IObservable<AsyncOperation> StartGame()
        {
            return Bootstrap.SceneManager.LoadScene(new Scene("Loading"));
        }
    }
}