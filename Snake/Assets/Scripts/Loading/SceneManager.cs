﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Game.System
{
    public class Scene
    {
        public readonly string sceneName;

        public Scene(string sceneName)
        {
            this.sceneName = sceneName;
        }
    }

    public class SceneManager
    {
        HashSet<Scene> loadedScenes;

        public SceneManager()
        {
            this.loadedScenes = new HashSet<Scene>();
        }

        public IObservable<AsyncOperation> LoadSceneAdditive(Scene scene)
        {
            return this.LoadSceneInternal(scene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        public IObservable<AsyncOperation> LoadScene(Scene scene)
        {
            return this.UnloadScenesInternal()
                .Concat(this.LoadSceneInternal(scene, UnityEngine.SceneManagement.LoadSceneMode.Single));
        }

        IObservable<AsyncOperation> UnloadScenesInternal()
        {
            IObservable<AsyncOperation> operation = Observable.Empty<AsyncOperation>();

            foreach (Scene scene in loadedScenes)
            {
                operation = operation.Merge(this.UnloadSceneInternal(scene));
            }

            return operation;
        }

        IObservable<AsyncOperation> UnloadSceneInternal(Scene scene)
        {
            return UnitySceneManager.UnloadSceneAsync(scene.sceneName).AsAsyncOperationObservable();
        }

        IObservable<AsyncOperation> LoadSceneInternal(Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            AsyncOperation loadOperation = UnitySceneManager.LoadSceneAsync(scene.sceneName, mode);
            return loadOperation.AsAsyncOperationObservable();
        }
    }
}