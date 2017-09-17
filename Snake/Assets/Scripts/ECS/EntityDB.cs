using System.Collections.Generic;
using UnityEngine.Assertions;
using UniRx;

namespace ECS
{
    using IdType = System.Int32;

    public interface IEntityDB
    {
        Entity CreateEntity(string humanReadableName);
        IObservable<Entity> GetEntities();

        void AddComponent<C>(Entity entity, C component) where C : IComponent;
        void RemoveComponent<C>(Entity entity) where C : IComponent;
        bool HasComponent<C>(Entity entity) where C : IComponent;
        C GetComponent<C>(Entity entity) where C : IComponent;
    }

    public class EntityDBImpl : IEntityDB
    {
        HashSet<Entity> entities;
        Dictionary<System.Type, Dictionary<Entity, IComponent>> components;
        IdType nextId;

        public EntityDBImpl()
        {
            this.entities = new HashSet<Entity>();
            this.components = new Dictionary<System.Type, Dictionary<Entity, IComponent>>();
            this.nextId = 0;
        }

        public Entity CreateEntity(string humanReadableName)
        {
            Entity entity = new Entity(nextId, humanReadableName);
            nextId++;
            this.entities.Add(entity);
            return entity;
        }

        public IObservable<Entity> GetEntities()
        {
            return this.entities.ToObservable();
        }

        public void AddComponent<C>(Entity entity, C component) where C : IComponent
        {
            System.Type componentType = typeof(C);
            if (!this.components.ContainsKey(componentType))
                this.components[componentType] = new Dictionary<Entity, IComponent>();

            if (this.components[componentType].ContainsKey(entity))
                throw new EntityDBException("Entity already had component.");

            this.components[componentType][entity] = component;
        }

        public void RemoveComponent<C>(Entity entity) where C : IComponent
        {
            System.Type componentType = typeof(C);
            if (!this.components.ContainsKey(componentType))
                throw new EntityDBException("No components of type " + componentType + " exist..");

            if (!this.components[componentType].ContainsKey(entity))
                throw new EntityDBException("Entity " + entity.humanReadableName + " does not have a component of type " + componentType);

            this.components[componentType].Remove(entity);
        }

        public bool HasComponent<C>(Entity entity) where C : IComponent
        {
            System.Type componentType = typeof(C);
            return this.components.ContainsKey(componentType) && this.components[componentType].ContainsKey(entity);
        }

        public C GetComponent<C>(Entity entity) where C : IComponent
        {
            System.Type componentType = typeof(C);
            return (C)this.components[componentType][entity];
        }
    }

    public class EntityDBException : System.Exception
    {
        public EntityDBException(string message) :
            base(message)
        {}
    }

    public static class EntityDBExtentions
    {
        public static IObservable<Entity> GetEntitiesWithComponent<C>(this IEntityDB entityDB) 
            where C : IComponent
        {
            return entityDB.GetEntities()
                .Where(entity => entityDB.HasComponent<C>(entity));
        }

        public static IObservable<Entity> GetEntitiesWithComponents<C1, C2>(this IEntityDB entityDB) 
            where C1 : IComponent
            where C2 : IComponent
        {
            return entityDB.GetEntities()
                .Where(entity => entityDB.HasComponent<C1>(entity) && entityDB.HasComponent<C2>(entity));
        }

        public static IObservable<Entity> GetEntitiesWithComponents<C1, C2, C3>(this IEntityDB entityDB)
            where C1 : IComponent
            where C2 : IComponent
            where C3 : IComponent
        {
            return entityDB.GetEntities()
                .Where(entity => 
                    entityDB.HasComponent<C1>(entity) && 
                    entityDB.HasComponent<C2>(entity) &&
                    entityDB.HasComponent<C3>(entity));
        }
    }
}