using General.Utilities;
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
        public InputActionData<float> Dash => dash ?? (dash = new InputActionData<float>(dashAction));
        public InputActionData<Vector2> Movement => movement ?? (movement = new InputActionData<Vector2>(movementAction));
        public InputActionData<Vector2> MousePointer => mousePointer ?? (mousePointer = new InputActionData<Vector2>(mousePointerAction));
        public InputActionData<float> LeftMouseButton => leftMouseButton ?? (leftMouseButton = new InputActionData<float>(leftMouseButtonAction));
        public InputActionData<float> RightMouseButton => rightMouseButton ?? (rightMouseButton = new InputActionData<float>(rightMouseButtonAction));
        public InputActionData<float> Attachment1 => attachment1 ?? (attachment1 = new InputActionData<float>(attachment1Action));
        public InputActionData<float> Attachment2 => attachment2 ?? (attachment2 = new InputActionData<float>(attachment2Action));
        public InputActionData<float> Attachment3 => attachment3 ?? (attachment3 = new InputActionData<float>(attachment3Action));
        public InputActionData<float> Mousewheel => mouswheel ?? (mouswheel = new InputActionData<float>(mousewheelAction));

        [SerializeField] private InputActionAsset inputActions;

        [SerializeField] private InputActionProperty dashAction;
        [SerializeField] private InputActionProperty movementAction;
        [SerializeField] private InputActionProperty mousePointerAction;
        [SerializeField] private InputActionProperty leftMouseButtonAction;
        [SerializeField] private InputActionProperty rightMouseButtonAction;
        [SerializeField] private InputActionProperty attachment1Action;
        [SerializeField] private InputActionProperty attachment2Action;
        [SerializeField] private InputActionProperty attachment3Action;
        [SerializeField] private InputActionProperty mousewheelAction;
        
        private InputActionData<float> dash;
        private InputActionData<Vector2> movement;
        private InputActionData<Vector2> mousePointer;
        private InputActionData<float> leftMouseButton;
        private InputActionData<float> rightMouseButton;
        private InputActionData<float> attachment1;
        private InputActionData<float> attachment2;
        private InputActionData<float> attachment3;
        private InputActionData<float> mouswheel;

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
                action?.Enable();
            }
        }

        public void DisableControls()
        {
            foreach (var action in inputActions)
            {
                action?.Disable();
            }
        }
    }
}