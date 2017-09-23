using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using UniRx;

namespace Framework
{
    public class AssetBundleException : System.Exception
    {
        public AssetBundleException(string message) : base(message) { }
    }

    public class AssetDefinition<T> where T : Object
    {
        public readonly string assetBundleName;
        public readonly string assetName;

        public AssetDefinition(string assetBundleName, string assetName)
        {
            this.assetBundleName = assetBundleName;
            this.assetName = assetName;
        }
    }

    public class AssetBundleManager
    {
        public IObservable<T> LoadAsset<T>(AssetDefinition<T> assetDefinition) where T : Object
        {
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetDefinition.assetBundleName, assetDefinition.assetName);
            if (assetPaths.Length == 0)
            {
                Assert.IsTrue(false, "There is no asset with name \"" + assetDefinition.assetName + "\" in " + assetDefinition.assetBundleName);
                return null;
            }
            
            foreach (var assetPath in assetPaths)
            {
                T target = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (target != null)
                    return Observable.Return<T>(target);
            }
            
            return Observable.Throw<T>(new AssetBundleException("Could not load asset of type " + typeof(T)));
        }

        public IObservable<AsyncOperation> LoadScene(Scene scene, bool isAdditive)
        {
            string[] levelPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(scene.assetBundleName, scene.sceneName);
            if (levelPaths.Length == 0)
            {
                Assert.IsTrue(false, "There is no scene " + scene.sceneName + " in asset bundle " + scene.assetBundleName);
                return Observable.Throw<AsyncOperation>(new AssetBundleException("Could not load scene " + scene.sceneName));
            }

            if (isAdditive)
                return EditorApplication.LoadLevelAdditiveAsyncInPlayMode(levelPaths[0]).AsAsyncOperationObservable();
            else
                return EditorApplication.LoadLevelAsyncInPlayMode(levelPaths[0]).AsAsyncOperationObservable();
        }
    }
}