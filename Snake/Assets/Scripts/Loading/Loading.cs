using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework
{
    public class GameLoader
    {
        readonly SceneManager sceneManager;
        IMainGame mainGameInstance;

        public GameLoader(SceneManager sceneManager, IMainGame mainGame)
        {
            this.sceneManager = sceneManager;
            this.mainGameInstance = mainGame;
        }

        public IObservable<IMainGame> LoadGame()
        {
            IObservable<AsyncOperation> sceneLoad = this.sceneManager.LoadScene(this.mainGameInstance.GetMainScene());
            IObservable<Unit> gameLoad = this.mainGameInstance.Load();

            return Observable.Create<IMainGame>(subscriber =>
                {
                    sceneLoad
                        .Select(_ => new Unit())
                        .Merge(gameLoad)
                        .Subscribe(_ => { }, _ => { },
                            () =>
                            {
                                subscriber.OnNext(this.mainGameInstance);
                                subscriber.OnCompleted();
                            });

                    return Disposable.Empty;
                });
        }
    }
}