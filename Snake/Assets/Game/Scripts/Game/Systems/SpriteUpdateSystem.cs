using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ViewSystems
{
    using Entitas;

    public class SpriteUpdateSystem : ReactiveSystem<ViewEntity>
    {
        public SpriteUpdateSystem(Contexts contexts) : base(contexts.view)
        {

        }

        protected override void Execute(List<ViewEntity> entities)
        {
            foreach (var viewEntity in entities)
            {
                if (!viewEntity.hasSpriteRendererLinkup)
                    viewEntity.AddSpriteRendererLinkup(new Components.SpriteRendererLinkup() { spriteRenderer = viewEntity.view.gameObject.GetComponent<SpriteRenderer>() });
                viewEntity.spriteRendererLinkup.value.spriteRenderer.sprite = viewEntity.spriteVisual.value.sprite;
                viewEntity.spriteRendererLinkup.value.spriteRenderer.sortingLayerName = viewEntity.spriteVisual.value.sortingLayer;
                viewEntity.spriteRendererLinkup.value.spriteRenderer.sortingOrder = viewEntity.spriteVisual.value.sortingOrder;
            }
        }

        protected override bool Filter(ViewEntity entity)
        {
            return entity.hasSpriteVisual && entity.hasView && entity.view.type == typeof(SpriteRenderer);
        }

        protected override ICollector<ViewEntity> GetTrigger(IContext<ViewEntity> context)
        {
            return context.CreateCollector(ViewMatcher.AllOf(ViewMatcher.SpriteVisual, ViewMatcher.View));
        }
    }
}