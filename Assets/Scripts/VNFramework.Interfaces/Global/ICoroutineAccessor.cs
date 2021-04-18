using System.Collections;
using UnityEngine;

namespace VNFramework.Interfaces.Global
{
    public interface ICoroutineAccessor
    {
        Coroutine StartCoroutine(IEnumerator coroutineFunction);
        void StopCoroutine(Coroutine coroutine);
    }
}
