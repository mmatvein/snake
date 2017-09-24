﻿using System.Collections;
using System.Collections.Generic;
using Framework;
using UniRx;
using Entitas;

namespace Game
{
    public class MainGame : ILoadableScene
    {
        private readonly MainApplication mainApplication;
        private Systems systems;
        private Contexts contexts;
        private System.IDisposable updateSubscription;

        public MainGame(MainApplication mainApplication)
        {
            this.mainApplication = mainApplication;
        }

        public Scene GetMainScene()
        {
            return new Scene("Game");
        }

        public IObservable<Unit> Load()
        {
            return Observable.Start(() =>
            {
                this.contexts = Contexts.sharedInstance;
                this.systems = this.CreateSystems();
                this.systems.Initialize();
            }, Scheduler.CurrentThread);
        }

        public void Start()
        {
            this.updateSubscription = Observable.EveryUpdate().Subscribe(this.Update);
        }

        public void End()
        {
            if (this.updateSubscription != null)
                this.updateSubscription.Dispose();
        }

        void Update(long updateTick)
        {
            this.systems.Execute();
            this.systems.Cleanup();
        }

        Systems CreateSystems()
        {
            return new Feature("Systems")
                .Add(new GameSystems.InputSystem(this.contexts))
                .Add(new GameSystems.TimerSystem(this.contexts, () => UnityEngine.Time.deltaTime))
                .Add(new GameSystems.TickerSystem(this.contexts))
                .Add(new ViewSystems.ViewManagementSystem(this.contexts, this.mainApplication.AssetBundleManager));
        }
    }
}