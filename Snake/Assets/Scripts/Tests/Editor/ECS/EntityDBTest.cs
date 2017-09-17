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

            this.entityDB.AddComponent(entity, new DummyComponent(0));

            Assert.IsTrue(this.entityDB.HasComponent<DummyComponent>(entity), "Could not find component on entity");

            try
            {
                this.entityDB.AddComponent(entity, new DummyComponent(0));
                Assert.IsTrue(false, "Was able to add duplicate component");
            }
            catch { }

            this.entityDB.RemoveComponent<DummyComponent>(entity);

            Assert.IsFalse(this.entityDB.HasComponent<DummyComponent>(entity), "Could still find component on entity");

            try
            {
                this.entityDB.RemoveComponent<DummyComponent>(entity);
                Assert.IsTrue(false, "Was able to remove an unexisting component");
            }
            catch { }
        }

        [Test]
        public void GetEntitiesWithComponents()
        {
            Entity firstEntity = this.entityDB.CreateEntity("First Entity");
            Entity secondEntity = this.entityDB.CreateEntity("Second Entity");

            this.entityDB.AddComponent(firstEntity, new DummyComponent(0));
            this.entityDB.AddComponent(firstEntity, new AnotherDummyComponent(0));
            this.entityDB.AddComponent(secondEntity, new DummyComponent(0));

            {
                IList<Entity> entitiesWithDummyComponent = this.entityDB
                    .GetEntitiesWithComponent<DummyComponent>()
                    .ToList()
                    .Wait();
                Assert.IsTrue(entitiesWithDummyComponent.Contains(firstEntity), "First entity wasn't in the list");
                Assert.IsTrue(entitiesWithDummyComponent.Contains(secondEntity), "Second entity wasn't in the list");
            }

            {
                IList<Entity> entitiesWithBothComponents = this.entityDB
                    .GetEntitiesWithComponents<DummyComponent, AnotherDummyComponent>()
                    .ToList()
                    .Wait();
                Assert.IsTrue(entitiesWithBothComponents.Contains(firstEntity), "First entity wasn't in the list");
                Assert.IsFalse(entitiesWithBothComponents.Contains(secondEntity), "Second entity was in the list");
            }
        }


        private class DummyComponent : Component<int>
        {
            public DummyComponent(int initialValue) : base(initialValue) { }
        }
        private class AnotherDummyComponent : Component<float>
        {
            public AnotherDummyComponent(float initialValue) : base(initialValue) { }
        }
    }
}