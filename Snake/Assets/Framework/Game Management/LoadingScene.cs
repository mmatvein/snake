using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Framework
{
    public class LoadingScene : ILoadableScene
    {
        readonly MainApplication mainApplication;

        public LoadingScene(MainApplication mainApplication)
        {
            this.mainApplication = mainApplication;
        }

        public Scene GetMainScene()
        {
            return new Scene("Loading");
        }

        public IObservable<Unit> Load()
        {
            return Observable.Empty<Unit>().Delay(System.TimeSpan.FromSeconds(2));
        }

        public void Start()
        {
            mainApplication.ChangeScene(new Game.MainGame(mainApplication)).Subscribe();
        }

        public void End()
        {
        }
    }
}