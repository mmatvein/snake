using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public class MainApplication : MonoBehaviour
    {
        public AssetBundleManager AssetBundleManager { get { return this.managers.assetBundleManager; } }
        SceneManager SceneManager { get { return this.managers.sceneManager; } }

        Managers managers;
        ILoadableScene currentScene;

        public void Init(AssetBundleManager assetBundleManager, SceneManager sceneManager)
        {
            this.managers = new Managers(assetBundleManager, sceneManager);
        }

        public IObservable<Unit> ChangeScene(ILoadableScene scene)
        {
            return Observable.Start(this.EndCurrentScene, Scheduler.MainThread)
                .Concat(this.SceneManager.LoadScene(scene.GetMainScene()).Select(_ => new Unit()))
                .Concat(scene.Load())
                .Concat(Observable.Start(() => this.SetCurrentScene(scene), Scheduler.MainThread));
        }

        void SetCurrentScene(ILoadableScene scene)
        {
            this.currentScene = scene;
            this.currentScene.Start();
        }

        void EndCurrentScene()
        {
            if (this.currentScene != null)
                this.currentScene.End();
        }

        internal class Managers
        {
            internal readonly AssetBundleManager assetBundleManager;
            internal readonly SceneManager sceneManager;

            internal Managers(AssetBundleManager assetBundleManager, SceneManager sceneManager)
            {
                this.assetBundleManager = assetBundleManager;
                this.sceneManager = sceneManager;
            }
        }
    }

}
