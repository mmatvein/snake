using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public static class Bootstrap
    {
        public static IObservable<Unit> StartGame()
        {
            return Observable.Create<MainApplication>(subscriber =>
                    {
                        GameObject mainObject = new GameObject("Main Application");
                        GameObject.DontDestroyOnLoad(mainObject);
                        MainApplication mainApp = mainObject.AddComponent<MainApplication>();

                        AssetBundleManager assetBundleManager = new AssetBundleManager();
                        SceneManager sceneManager = new SceneManager(assetBundleManager);
                        mainApp.Init(assetBundleManager, sceneManager);

                        subscriber.OnNext(mainApp);
                        subscriber.OnCompleted();

                        return Disposable.Empty;
                    })
                .SelectMany(mainApp => mainApp.ChangeScene(new LoadingScene(mainApp)));
        }
    }
}