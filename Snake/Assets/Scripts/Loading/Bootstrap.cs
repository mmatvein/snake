using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.System
{
    public static class Bootstrap
    {
        static SceneManager sceneManager;

        static Bootstrap()
        {
            sceneManager = new SceneManager();
        }

        public static IObservable<AsyncOperation> StartGame()
        {
            return sceneManager.LoadScene(new Scene("Loading"));
        }
    }
}