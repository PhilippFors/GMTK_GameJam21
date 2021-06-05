using Utilities;
using General.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerInput
{
    /// <summary>
    /// Contains all of the inputs the player can use.
    /// Class is singleton so other classes can easily access input actions.
    /// </summary>
    public class PlayerInputController : SingletonBehaviour<PlayerInputController>
    {
        public InputActionData<float> Jump => jump ?? (jump = new InputActionData<float>(jumpAction));
        public InputActionData<float> Dash => dash ?? (dash = new InputActionData<float>(dashAction));
        public InputActionData<float> Movement => movement ?? (movement = new InputActionData<float>(movementAction));
        public InputActionData<Vector2> MousePointer => mousePointer ?? (mousePointer = new InputActionData<Vector2>(mousePointerAction));
        public InputActionData<float> LeftMouseButton => leftMouseButton ?? (leftMouseButton = new InputActionData<float>(leftMouseButtonAction));
        public InputActionData<float> RightMouseButton => rightMouseButton ?? (rightMouseButton = new InputActionData<float>(rightMouseButtonAction));
        public InputActionData<float> ToggleCenterCamera => toggleCenterCamera ?? (toggleCenterCamera = new InputActionData<float>(toggleCenterCameraAction));

        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private InputActionProperty jumpAction;
        [SerializeField] private InputActionProperty dashAction;
        [SerializeField] private InputActionProperty movementAction;
        [SerializeField] private InputActionProperty mousePointerAction;
        [SerializeField] private InputActionProperty leftMouseButtonAction;
        [SerializeField] private InputActionProperty rightMouseButtonAction;
        [SerializeField] private InputActionProperty toggleCenterCameraAction;

        private InputActionData<float> jump;
        private InputActionData<float> dash;
        private InputActionData<float> movement;
        private InputActionData<Vector2> mousePointer;
        private InputActionData<float> leftMouseButton;
        private InputActionData<float> rightMouseButton;
        private InputActionData<float> toggleCenterCamera;

        private void OnEnable()
        {
            EnableControls();
        }

        private void OnDisable()
        {
            DisableControls();
        }
        
        public void EnableControls()
        {
            foreach (var action in inputActions)
            {
                if (action != null)
                {
                    action.Enable();
                }
            }
        }

        public void DisableControls()
        {
            foreach (var action in inputActions)
            {
                if (action != null)
                {
                    action.Disable();
                }
            }
        }
    }
}