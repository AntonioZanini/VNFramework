using VNFramework.Interfaces.Global;
using System.Collections;
using UnityEngine;
using VNFramework.Core.Helpers;

namespace VNFramework.Core.Settings
{
    public class CoroutineAccessor : ICoroutineAccessor
    {
        public Coroutine StartCoroutine(IEnumerator coroutineFunction)
        {
            return BaseHelpers.StartCoroutine(coroutineFunction);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            BaseHelpers.StopCoroutine(coroutine);
        }
    }
}
