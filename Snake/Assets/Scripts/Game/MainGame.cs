

namespace Game
{
    using System.Collections.Generic;
    using UniRx;
    using ECS;
    using Systems;
    using Components;
    using Components.Visuals;
    using Vector2 = UnityEngine.Vector2;

    public class MainGame : Framework.IMainGame
    {
        public const float TickTime = 0.5f;

        List<ISystemContinuous> continuousSystems;
        List<ISystemReactive> reactiveSystems;
        IEntityDB entityDB;
        
        System.IDisposable deltaTimeUpdater;

        public MainGame()
        {
        }

        public IObservable<Unit> Load()
        {
            return Observable.Create<Unit>(subscriber =>
                {
                    this.entityDB = new EntityDBImpl();
                    this.SetupContinuousSystems();
                    this.SetupReactiveSystems();

                    this.CreateSnake();
                    this.CreateTicker();

                    subscriber.OnCompleted();

                    return Disposable.Empty;
                });
        }

        void CreateSnake()
        {
            Entity snake = this.entityDB.CreateEntity("Snake");
            this.entityDB.AddComponent(
                snake,
                new SnakePosition(
                        new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) }
                )
            );
            this.entityDB.AddComponent(
                snake,
                new Velocity(new Vector2(-1, 0))
            );

            this.entityDB.AddComponent(
                snake,
                new SnakeVisuals()
            );

            this.entityDB.AddComponent(
                snake,
                new Components.Input()
            );

            this.entityDB.AddComponent(
                snake,
                new SnakeDirection(Direction.Left, Direction.Left)
            );
        }

        void CreateTicker()
        {
            Entity ticker = this.entityDB.CreateEntity("Ticker");
            this.entityDB.AddComponent(
                ticker,
                new Timer(0)
            );

            this.entityDB.AddComponent(
                ticker,
                new Ticker(0.2f, 0)
            );
        }

        public void StartGame()
        {
            if (this.deltaTimeUpdater != null)
                this.deltaTimeUpdater.Dispose();

            this.deltaTimeUpdater = Observable.EveryUpdate().Subscribe(
                _ =>
                    {
                        foreach (var deltaTimeUpdateSystem in this.continuousSystems)
                            deltaTimeUpdateSystem.Update(UnityEngine.Time.deltaTime);
                    }
                );

            foreach (var reactiveSystem in this.reactiveSystems)
            {
                reactiveSystem.Register();
            }
        }

        public Framework.Scene GetMainScene()
        {
            return new Framework.Scene("Game");
        }

        void SetupContinuousSystems()
        {
            this.continuousSystems = new List<ISystemContinuous>
            {
                new InputSystem(this.entityDB),
                new TimerIncrementSystem(this.entityDB),
                new SnakeDirectionSystem(this.entityDB),
                new SnakeRenderSystem(this.entityDB),
                new TickerSystem(this.entityDB)
            };

        }

        void SetupReactiveSystems()
        {
            this.reactiveSystems = new List<ISystemReactive>
            {
                new SnakeVelocitySystem(this.entityDB),
                new SnakeMoveSystem(this.entityDB)
            };
        }
    }
}