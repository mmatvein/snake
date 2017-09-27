using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    [View]
    public class SpriteRendererLinkup
    {
        public SpriteRenderer spriteRenderer;
    }

    [View]
    public class SpriteVisual
    {
        public Sprite sprite;
        public string sortingLayer;
        public int sortingOrder;
    }
}