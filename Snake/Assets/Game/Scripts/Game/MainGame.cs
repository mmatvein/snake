using System.Collections;
using System.Collections.Generic;
using Framework;
using UniRx;
using Entitas;

namespace Game
{
    public class MainGame : Framework.IMainGame
    {
        private Systems systems;
        private Contexts contexts;

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

        public void StartGame()
        {
            Observable.EveryUpdate().Subscribe(this.Update);
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
                .Add(new ViewSystems.ViewManagementSystem(this.contexts));
        }
    }
}