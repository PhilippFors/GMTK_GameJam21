using System;
using UnityEngine.InputSystem;

namespace General.Input
{
    /// <summary>
    /// This class provides the usual input action functionality but extends it with extra functionality.
    /// </summary>
    public class InputActionData<T> where T : struct
    {
        public event Action<InputAction.CallbackContext> Started;
        public event Action<InputAction.CallbackContext> Performed;
        public event Action<InputAction.CallbackContext> Canceled;

        public bool IsPressed { get; private set; }
        public bool Triggered => action.triggered;
        private InputAction action;

        public InputActionData(InputActionProperty property)
        {
            this.action = property.action;
            action.started += ctx => Started?.Invoke(ctx);
            action.started += ctx => IsPressed = true;
            
            action.performed += ctx => Performed?.Invoke(ctx);
            
            action.canceled += ctx => Canceled?.Invoke(ctx);
            action.canceled += ctx => IsPressed = false;
        }

        public T ReadValue() => action.ReadValue<T>();
    }
}
