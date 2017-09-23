using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Game.ViewSystems
{
    using Entitas;
    using Entitas.Unity;
    using Framework;

    public class ViewManagementSystem : Feature
    {
        public ViewManagementSystem(Contexts contexts, AssetBundleManager assetBundleManager) : base("View Management")
        {
            this.Add(new ViewPoolingSystem<SpriteRenderer>(contexts, assetBundleManager, new AssetDefinition<SpriteRenderer>("prefabs", "Pooled Sprite")));
        }
    }

    internal class ViewPoolingSystem<PooledObjectType> : ReactiveSystem<ViewEntity>, IInitializeSystem where PooledObjectType : Component
    {
        readonly ViewContext viewContext;
        readonly AssetBundleManager assetBundleManager;
        readonly AssetDefinition<PooledObjectType> assetDefinition;

        ViewEntity viewPoolEntity;

        public ViewPoolingSystem(Contexts contexts, AssetBundleManager assetBundleManager, AssetDefinition<PooledObjectType> assetDefinition) : base(contexts.view)
        {
            this.viewContext = contexts.view;
            this.assetBundleManager = assetBundleManager;
            this.assetDefinition = assetDefinition;
        }

        public void Initialize()
        {
            this.viewPoolEntity = this.viewContext.CreateEntity();
            this.viewPoolEntity.AddViewPool(this.assetBundleManager.LoadAsset(this.assetDefinition).Wait(), new List<GameObject>(), new List<GameObject>());
        }

        protected override void Execute(List<ViewEntity> entities)
        {
            foreach (ViewEntity viewEntity in entities)
            {
                GameObject view = this.GetNew();
                viewEntity.AddView(view);
                view.Link(viewEntity, this.viewContext);
                viewEntity.RemoveCreateView();
                viewEntity.OnComponentRemoved += this.OnComponentRemoved;
            }
        }

        private void OnComponentRemoved(IEntity entity, int index, IComponent component)
        {
            if (component is Components.ViewComponent)
            {
                Components.ViewComponent viewComponent = component as Components.ViewComponent;
                viewComponent.gameObject.Unlink();
                this.Release(viewComponent.gameObject);
                entity.OnComponentRemoved -= this.OnComponentRemoved;
            }
        }

        protected override bool Filter(ViewEntity entity)
        {
            return entity.hasCreateView && entity.createView.type == typeof(PooledObjectType);
        }

        protected override ICollector<ViewEntity> GetTrigger(IContext<ViewEntity> context)
        {
            return context.CreateCollector(ViewMatcher.CreateView);
        }

        private GameObject GetNew()
        {
            GameObject viewToUse = null;
            if (this.viewPoolEntity.viewPool.freeViews.Count == 0)
            {
                viewToUse = GameObject.Instantiate<PooledObjectType>(this.viewPoolEntity.viewPool.prefab as PooledObjectType).gameObject;
            }
            else
            {
                viewToUse = this.viewPoolEntity.viewPool.freeViews[0];
                this.viewPoolEntity.viewPool.freeViews.RemoveAt(0);
            }

            viewToUse.SetActive(true);
            this.viewPoolEntity.viewPool.usedViews.Add(viewToUse);
            return viewToUse;
        }

        private void Release(GameObject viewObject)
        {
            Assert.IsTrue(this.viewPoolEntity.viewPool.usedViews.Contains(viewObject), "View object was not in used views");
            this.viewPoolEntity.viewPool.usedViews.Remove(viewObject);
            this.viewPoolEntity.viewPool.freeViews.Add(viewObject);
            viewObject.SetActive(false);
        }
    }
}
