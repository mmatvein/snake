using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game
{
    using ECS;
    using Systems;
    using Components;
    using Components.Visuals;

    public class MainGame : Framework.IMainGame
    {
        public const float TickTime = 0.5f;

        List<ISystemDeltaTime> deltaTimeUpdateSystems;
        List<ISystemTicks> tickUpdateSystems;
        IEntityDB entityDB;

        System.IDisposable tickUpdater;
        System.IDisposable deltaTimeUpdater;

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
                    this.entityDB.AddComponent(
                        snake, 
                        new Component<SnakePosition>(
                            new SnakePosition(
                                new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) }
                            )
                        )
                    );
                    this.entityDB.AddComponent(
                        snake,
                        new Component<Velocity>(
                            new Velocity(new Vector2(-1, 0))
                        )
                    );

                    this.entityDB.AddComponent(
                        snake,
                        new Component<SnakeVisuals>(
                            new SnakeVisuals()
                        )
                    );

                    this.entityDB.AddComponent(
                        snake,
                        new Component<Components.Input>(
                            new Components.Input()
                        )
                    );

                    this.entityDB.AddComponent(
                        snake,
                        new Component<SnakeDirection>(
                            new SnakeDirection(Direction.Left, Direction.Left)
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

            this.tickUpdater = Observable.Interval(System.TimeSpan.FromSeconds(TickTime)).Subscribe(
                _ =>
                    {
                        foreach (var tickUpdateSystem in this.tickUpdateSystems)
                            tickUpdateSystem.Update();
                    }
                );

            if (this.deltaTimeUpdater != null)
                this.deltaTimeUpdater.Dispose();

            this.deltaTimeUpdater = Observable.EveryUpdate().Subscribe(
                tick =>
                    {
                        foreach (var deltaTimeUpdateSystem in this.deltaTimeUpdateSystems)
                            deltaTimeUpdateSystem.Update(tick / 1000f);
                    }
                );
        }

        public Framework.Scene GetMainScene()
        {
            return new Framework.Scene("Game");
        }

        void SetupDeltaTimeUpdateSystems()
        {
            this.deltaTimeUpdateSystems = new List<ISystemDeltaTime>
            {
                new InputSystem(this.entityDB),
                new SnakeDirectionSystem(this.entityDB),
                new SnakeRenderSystem(this.entityDB)
            };

        }

        void SetupTickUpdateSystems()
        {
            this.tickUpdateSystems = new List<ISystemTicks>
            {
                new SnakeVelocitySystem(this.entityDB),
                new SnakeMoveSystem(this.entityDB)
            };
        }
    }
}