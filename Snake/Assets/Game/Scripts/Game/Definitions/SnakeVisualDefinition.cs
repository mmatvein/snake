using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Definitions
{
    [CreateAssetMenu]
    public class SnakeVisualDefinition : ScriptableObject
    {
        public Sprite straightLeft;
        public Sprite straightRight;
        public Sprite straightUp;
        public Sprite straightDown;

        public Sprite upToRight;
        public Sprite upToLeft;
        public Sprite downToRight;
        public Sprite downToLeft;

        public Sprite leftToUp;
        public Sprite leftToDown;
        public Sprite rightToUp;
        public Sprite rightToDown;

        public Sprite headLeft;
        public Sprite headRight;
        public Sprite headUp;
        public Sprite headDown;

        public Sprite tailLeft;
        public Sprite tailRight;
        public Sprite tailUp;
        public Sprite tailDown;
    }
}