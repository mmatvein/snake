using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using UniRx;

namespace ECS.Test
{
    public class EntityDBTest
    {
        IEntityDB entityDB;

        [SetUp]
        public void SetUp()
        {
            this.entityDB = new EntityDBImpl();
        }

        [Test]
        public void EntityDBCreateEntity()
        {
            int countOfEntitiesToCreate = 10000;
            for (int i = 0; i < countOfEntitiesToCreate; i++)
                this.entityDB.CreateEntity("Entity " + i);


            int uniqueEntityCount = this.entityDB.GetEntities()
                .Distinct()
                .Select(entity => entity.id)
                .Aggregate((_, value) => value + 1)
                .Wait();

            Assert.AreEqual(countOfEntitiesToCreate, uniqueEntityCount, "Clashes in entity IDs");
        }

        [Test]
        public void AddRemoveComponent()
        {
            Entity entity = this.entityDB.CreateEntity("Test Entity");

            this.entityDB.AddComponent(entity, 0);

            Assert.IsTrue(this.entityDB.HasComponent<int>(entity), "Could not find component on entity");

            try
            {
                this.entityDB.AddComponent(entity, 0);
                Assert.IsTrue(false, "Was able to add duplicate component");
            }
            catch { }

            this.entityDB.RemoveComponent<int>(entity);

            Assert.IsFalse(this.entityDB.HasComponent<int>(entity), "Could still find component on entity");

            try
            {
                this.entityDB.RemoveComponent<int>(entity);
                Assert.IsTrue(false, "Was able to remove an unexisting component");
            }
            catch { }
        }

        [Test]
        public void GetEntitiesWithComponents()
        {
            Entity firstEntity = this.entityDB.CreateEntity("First Entity");
            Entity secondEntity = this.entityDB.CreateEntity("Second Entity");
            Entity thirdEntity = this.entityDB.CreateEntity("Third Entity");

            this.entityDB.AddComponent(firstEntity, 0f);
            this.entityDB.AddComponent(firstEntity, 0);
            this.entityDB.AddComponent(firstEntity, 0L);
            this.entityDB.AddComponent(secondEntity, 0f);
            this.entityDB.AddComponent(secondEntity, 0L);
            this.entityDB.AddComponent(thirdEntity, 0L);

            {
                IList<Entity> entities = 
                    this.entityDB.GetEntitiesWithComponent<float>()
                        .Select(tuple => tuple.Item1)  
                        .ToList()
                        .Wait();
                Assert.IsTrue(entities.Contains(firstEntity), "First entity wasn't in the list");
                Assert.IsTrue(entities.Contains(secondEntity), "Second entity wasn't in the list");
                Assert.IsFalse(entities.Contains(thirdEntity), "Third entity was in the list");
            }

            {
                IList<Entity> entities = this.entityDB
                    .GetEntitiesWithComponents<float, int>()
                    .Select(tuple => tuple.Item1)
                    .ToList()
                    .Wait();
                Assert.IsTrue(entities.Contains(firstEntity), "First entity wasn't in the list");
                Assert.IsFalse(entities.Contains(secondEntity), "Second entity was in the list");
                Assert.IsFalse(entities.Contains(thirdEntity), "Third entity was in the list");
            }

            {
                IList<Entity> entities = this.entityDB
                    .GetEntitiesWithComponents<float, int, long>()
                    .Select(tuple => tuple.Item1)
                    .ToList()
                    .Wait();
                Assert.IsTrue(entities.Contains(firstEntity), "First entity wasn't in the list");
                Assert.IsFalse(entities.Contains(secondEntity), "Second entity was in the list");
                Assert.IsFalse(entities.Contains(thirdEntity), "Third entity was in the list");
            }
        }

        [Test]
        public void GetObservableComponentStream()
        {
            Entity firstEntity = this.entityDB.CreateEntity("First entity");

            this.entityDB.AddComponent(firstEntity, 0);

            System.Action update = () =>
                {
                    this.entityDB.GetEntitiesWithComponent<int>()
                        .Subscribe(entity => entity.Item2.SetValue(entity.Item2.Value + 1));
                };

            int callCount = 0;
            System.IDisposable subscription = 
                this.entityDB.GetObservableComponentStream<int>()
                    .Subscribe(component => callCount++);

            update();
            update();

            subscription.Dispose();

            Assert.AreEqual(2, callCount, "Component update event not triggered correct amount of times");
        }
    }
}