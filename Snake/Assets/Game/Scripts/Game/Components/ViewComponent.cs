using UnityEngine;
using System.Collections.Generic;

namespace Game.Components
{
    [View]
    public class CreateViewComponent : Entitas.IComponent
    {
        public System.Type type;
    }
    
    [View]
    public class ViewComponent : Entitas.IComponent
    {
        public GameObject gameObject;
    }

    [View]
    public class ViewPool : Entitas.IComponent
    {
        public Object prefab;
        public List<GameObject> freeViews;
        public List<GameObject> usedViews;
        public Transform poolTransform;
    }
}