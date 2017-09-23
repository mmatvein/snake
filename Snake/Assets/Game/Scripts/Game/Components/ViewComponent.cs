using UnityEngine;
using System.Collections.Generic;

namespace Game.Components
{
    [View]
    public class CreateViewComponent : Entitas.IComponent { }
    
    [View]
    public class ViewComponent : Entitas.IComponent
    {
        public GameObject gameObject;
    }

    [View, Entitas.CodeGeneration.Attributes.Unique]
    public class ViewPool : Entitas.IComponent
    {
        public List<GameObject> freeViews;
        public List<GameObject> usedViews;
    }
}