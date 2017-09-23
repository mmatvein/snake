using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.ViewSystems
{
    using Entitas;
    using Entitas.Unity;

    public class ViewManagementSystem : Feature
    {
        public ViewManagementSystem(Contexts contexts) : base("View Management")
        {
            this.Add(new ViewPoolingSystem(contexts));
        }
    }

    internal class ViewPoolingSystem : ReactiveSystem<ViewEntity>, IInitializeSystem
    {
        readonly ViewContext viewContext;

        public ViewPoolingSystem(Contexts contexts) : base(contexts.view)
        {
            this.viewContext = contexts.view;
        }

        public void Initialize()
        {
            if (!this.viewContext.hasViewPool)
            {
                this.viewContext.SetViewPool(new List<GameObject>(), new List<GameObject>());
            }
        }

        protected override void Execute(List<ViewEntity> entities)
        {
            foreach (ViewEntity viewEntity in entities)
            {
                GameObject view = this.GetNew();
                viewEntity.AddView(view);
                view.Link(viewEntity, this.viewContext);
                viewEntity.isCreateView = false;
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
            return entity.isCreateView;
        }

        protected override ICollector<ViewEntity> GetTrigger(IContext<ViewEntity> context)
        {
            return context.CreateCollector(ViewMatcher.CreateView);
        }

        private GameObject GetNew()
        {
            GameObject viewToUse = null;
            if (this.viewContext.viewPool.freeViews.Count == 0)
            {
                viewToUse = new GameObject("Game View");
                
            }
            else
            {
                viewToUse = this.viewContext.viewPool.freeViews[0];
                this.viewContext.viewPool.freeViews.RemoveAt(0);
            }

            viewToUse.SetActive(true);
            this.viewContext.viewPool.usedViews.Add(viewToUse);
            return viewToUse;
        }

        private void Release(GameObject viewObject)
        {
            Assert.IsTrue(this.viewContext.viewPool.usedViews.Contains(viewObject), "View object was not in used views");
            this.viewContext.viewPool.usedViews.Remove(viewObject);
            this.viewContext.viewPool.freeViews.Add(viewObject);
            viewObject.SetActive(false);
        }
    }
}
