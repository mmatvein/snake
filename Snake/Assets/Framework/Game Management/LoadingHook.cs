using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Framework
{
    public class LoadingHook : MonoBehaviour
    {
        private void Start()
        {
            GameLoader gameLoader = new GameLoader(Bootstrap.SceneManager, new Game.MainGame(Bootstrap.AssetBundleManager));

            gameLoader.LoadGame().Subscribe(
                game => game.StartGame(),
                _ => Debug.LogError("ERROR!"),
                () => Debug.Log("Main Game loaded")
            );
        }
    }
}