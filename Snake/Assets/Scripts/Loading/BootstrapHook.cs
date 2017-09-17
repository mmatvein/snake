using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Framework
{
    public class BootstrapHook : MonoBehaviour
    {
        private void Start()
        {
            Bootstrap.StartGame().Subscribe(
                _ => Debug.Log("onNext"),
                _ => Debug.LogError("ERROR!"),
                () => Debug.Log("Scene loaded")
            );
        }
    }
}