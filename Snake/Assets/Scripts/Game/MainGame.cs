using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
    using ECS;
    using Systems;
    public class MainGame : Framework.IMainGame
    {
        public const float TickTime = 1;

        List<ISystemDeltaTime> deltaTimeUpdateSystems;
        List<ISystemTicks> tickUpdateSystems;
        IEntityDB entityDB;

        System.IDisposable tickUpdater;

        public MainGame()
        {
        }

        public IObservable<Unit> Load()
        {
            return Observable.Create<Unit>(subscriber =>
                {
                    this.entityDB = new EntityDBImpl();
                    this.SetupDeltaTimeUpdateSystems();
                    this.SetupTickUpdateSystems();

                    Entity snake = this.entityDB.CreateEntity("Snake");
                    this.entityDB.AddComponent<Component<Components.SnakePosition>>(
                        snake, 
                        new Component<Components.SnakePosition>(
                            new Components.SnakePosition(
                                new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) }
                            )
                        )
                    );
                    this.entityDB.AddComponent<Component<Components.Velocity>>(
                        snake,
                        new Component<Components.Velocity>(
                            new Components.Velocity(new Vector2(-1, 0))
                        )
                    );

                    subscriber.OnCompleted();

                    return Disposable.Empty;
                });
        }

        public void StartGame()
        {
            if (this.tickUpdater != null)
                this.tickUpdater.Dispose();

            this.tickUpdater = Observable.Interval(System.TimeSpan.FromSeconds(TickTime)).Subscribe
                (_ =>
                    {
                        foreach (var tickUpdateSystem in this.tickUpdateSystems)
                            tickUpdateSystem.Update();
                    }
                );
        }

        public Framework.Scene GetMainScene()
        {
            return new Framework.Scene("Game");
        }

        void SetupDeltaTimeUpdateSystems()
        {

        }

        void SetupTickUpdateSystems()
        {
            this.tickUpdateSystems = new List<ISystemTicks>();

            this.tickUpdateSystems.Add(new SnakeMoveSystem(this.entityDB));
        }
    }
}