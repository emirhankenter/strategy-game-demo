using System;
using UnityEngine;

namespace Scripts
{
    public class ErrorEvents : MonoBehaviour
    {
        public static ErrorEvents Current;

        private void Awake()
        {
            Current = this;
        }

        public event Action OnOutOfSpaceTriggerEnter;

        public void OutOfSpaceTriggerEnter()
        {
            OnOutOfSpaceTriggerEnter?.Invoke();
        }
    }
}
