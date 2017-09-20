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

        void AddComponent<ComponentContent>(Entity entity, ComponentContent componentContent);
        void RemoveComponent<ComponentContent>(Entity entity);
        bool HasComponent<ComponentContent>(Entity entity);

        Component<ComponentContent> GetComponent<ComponentContent>(Entity entity);
        IObservable<Component<ComponentContent>> GetObservableComponentStream<ComponentContent>();
    }

    public class EntityDBImpl : IEntityDB
    {
        HashSet<Entity> entities;
        Dictionary<System.Type, Dictionary<Entity, IComponent>> components;
        Dictionary<System.Type, Subject<IComponent>> componentUpdateStreams;
        IdType nextId;

        public EntityDBImpl()
        {
            this.entities = new HashSet<Entity>();
            this.components = new Dictionary<System.Type, Dictionary<Entity, IComponent>>();
            this.componentUpdateStreams = new Dictionary<System.Type, Subject<IComponent>>();
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

        public void AddComponent<ComponentContent>(Entity entity, ComponentContent componentContent)
        {
            System.Type componentType = typeof(Component<ComponentContent>);
            if (!this.components.ContainsKey(componentType))
                this.components[componentType] = new Dictionary<Entity, IComponent>();

            if (this.components[componentType].ContainsKey(entity))
                throw new EntityDBException("Entity already had component.");

            Component<ComponentContent> component = new Component<ComponentContent>(componentContent);
            this.components[componentType][entity] = component;

            component.OnValueUpdated += this.OnComponentValueUpdated;

            this.SignalChange(component);
        }

        public void RemoveComponent<ComponentContent>(Entity entity)
        {
            System.Type componentType = typeof(Component<ComponentContent>);
            if (!this.components.ContainsKey(componentType))
                throw new EntityDBException("No components of type " + componentType + " exist..");

            if (!this.components[componentType].ContainsKey(entity))
                throw new EntityDBException("Entity " + entity.humanReadableName + " does not have a component of type " + componentType);

            (this.components[componentType][entity] as Component<ComponentContent>).OnValueUpdated -= this.OnComponentValueUpdated;

            this.components[componentType].Remove(entity);
        }

        public bool HasComponent<ComponentContent>(Entity entity)
        {
            System.Type componentType = typeof(Component<ComponentContent>);
            return this.components.ContainsKey(componentType) && this.components[componentType].ContainsKey(entity);
        }

        public Component<ComponentContent> GetComponent<ComponentContent>(Entity entity)
        {
            System.Type componentType = typeof(Component<ComponentContent>);
            return (Component<ComponentContent>)this.components[componentType][entity];
        }

        public IObservable<Component<ComponentContent>> GetObservableComponentStream<ComponentContent>()
        {
            System.Type componentType = typeof(Component<ComponentContent>);
            if (!this.componentUpdateStreams.ContainsKey(componentType))
            {
                this.componentUpdateStreams.Add(componentType, new Subject<IComponent>());
            }

            return this.componentUpdateStreams[componentType].AsObservable().Cast<IComponent, Component<ComponentContent>>();
        }

        void OnComponentValueUpdated<C>(C component) where C : IComponent
        {
            this.SignalChange(component);
        }

        private void SignalChange<C>(C component) where C : IComponent
        {
            System.Type componentType = typeof(C);
            if (this.componentUpdateStreams.ContainsKey(componentType))
            {
                this.componentUpdateStreams[componentType].OnNext(component);
            }
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
        {
            return entityDB.GetEntities()
                .Where(entity => entityDB.HasComponent<C>(entity));
        }

        public static IObservable<Entity> GetEntitiesWithComponents<C1, C2>(this IEntityDB entityDB)
        {
            return entityDB.GetEntities()
                .Where(entity => entityDB.HasComponent<C1>(entity) && entityDB.HasComponent<C2>(entity));
        }

        public static IObservable<Entity> GetEntitiesWithComponents<C1, C2, C3>(this IEntityDB entityDB)
        {
            return entityDB.GetEntities()
                .Where(entity => 
                    entityDB.HasComponent<C1>(entity) && 
                    entityDB.HasComponent<C2>(entity) &&
                    entityDB.HasComponent<C3>(entity));
        }
    }
}