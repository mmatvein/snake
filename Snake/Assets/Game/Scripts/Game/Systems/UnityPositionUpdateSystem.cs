using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystems
{
    using Entitas;
    public class UnityPositionUpdateSystem : ReactiveSystem<ViewEntity>
    {
        public UnityPositionUpdateSystem(Contexts contexts) : base(contexts.view)
        {

        }

        protected override void Execute(List<ViewEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.unityTransform.value.position = entity.position.value;
            }
        }

        protected override bool Filter(ViewEntity entity)
        {
            return entity.hasPosition && entity.hasUnityTransform;
        }

        protected override ICollector<ViewEntity> GetTrigger(IContext<ViewEntity> context)
        {
            return context.CreateCollector(ViewMatcher.AllOf(ViewMatcher.Position, ViewMatcher.UnityTransform));
        }
    }
}