using System.Collections;
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
            }, Scheduler.MainThread);
        }

        public void Start()
        {
            this.updateSubscription = Observable.EveryUpdate().Subscribe(this.Update);
        }

        public IObservable<Unit> Unload()
        {
            return Observable.Start(() =>
            {
                if (this.updateSubscription != null)
                    this.updateSubscription.Dispose();

                this.systems.TearDown();
            }, Scheduler.MainThread);
        }

        void Update(long updateTick)
        {
            this.systems.Execute();
            this.systems.Cleanup();
        }

        Systems CreateSystems()
        {
            return new Feature("Systems")
                .Add(new InputSystems.InputSystem(this.contexts))
                .Add(new GameSystems.GameSystems(this.contexts, this.mainApplication.AssetBundleManager))
                .Add(new ViewSystems.ViewManagementSystem(this.contexts, this.mainApplication.AssetBundleManager))
                .Add(new ViewSystems.SpriteUpdateSystem(this.contexts))
                .Add(new GameSystems.UnityPositionUpdateSystem(this.contexts));
        }
    }
}