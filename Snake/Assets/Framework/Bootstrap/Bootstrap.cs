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
            AssetBundleManager assetBundleManager = new AssetBundleManager();
            Bootstrap.SceneManager = new SceneManager(assetBundleManager);
        }

        public static IObservable<AsyncOperation> StartGame()
        {
            return Bootstrap.SceneManager.LoadScene(new Scene("Loading"));
        }
    }
}