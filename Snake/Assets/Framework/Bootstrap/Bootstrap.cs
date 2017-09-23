using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public static class Bootstrap
    {
        public static readonly SceneManager SceneManager;
        public static readonly AssetBundleManager AssetBundleManager;

        static Bootstrap()
        {
            Bootstrap.AssetBundleManager = new AssetBundleManager();
            Bootstrap.SceneManager = new SceneManager(Bootstrap.AssetBundleManager);
        }

        public static IObservable<AsyncOperation> StartGame()
        {
            return Bootstrap.SceneManager.LoadScene(new Scene("Loading"));
        }
    }
}