using UnityEngine;
using Entities.Player.PlayerInput;

namespace Entities.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        [Header("Grounded settings")]
        [SerializeField] private LayerMask groundedMask;
        [SerializeField] private float checkSphereYPos;
        [SerializeField] private float checkSphereRadius;
        
        private Vector2 MovementDir => PlayerInputController.Instance.Movement.ReadValue();
        private Vector2 MousePointer => PlayerInputController.Instance.MousePointer.ReadValue();
        private Vector3 velocity;
        private Vector3 currentMoveDirection;
        private Camera mainCam;
        private bool isDashing;
        private bool isGrounded;

        private void Awake()
        {
            mainCam = Camera.main;
            PlayerInputController.Instance.Dash.Performed += ctx => Dash();
        }
        
        private void Update()
        {
            IsGrounded();
            Move();
            UpdateLookDirection();
        }

        private void Dash()
        {
            // TODO: Dash
        }
        
        private void Move()
        {
            transform.position += (currentMoveDirection + velocity).normalized * (movementSpeed * Time.deltaTime);

            var move = MovementDir;
            var direction = new Vector3(move.x, 0, move.y);

            if (!isDashing)
            {
                currentMoveDirection = direction * movementSpeed;
            }
        }

        private void IsGrounded()
        {
            if (Physics.CheckSphere(transform.position - new Vector3(0, checkSphereYPos, 0), checkSphereRadius, groundedMask))
            {
                if (!isGrounded)
                {
                    velocity.y = 0;
                }
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
                velocity.y += Physics.gravity.y * Time.deltaTime;
            }
        }

        private void UpdateLookDirection()
        {
            var pointToLook = Vector3.zero;
            var plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
            var cameraRay = mainCam.ScreenPointToRay(MousePointer);

            if (plane.Raycast(cameraRay, out var rayLength))
            {
                var rayPoint = cameraRay.GetPoint(rayLength);
                pointToLook = rayPoint - transform.position;
            }

            if (pointToLook.magnitude > 0.0001)
            {
                var newRot = Quaternion.LookRotation(pointToLook);
                transform.rotation = newRot;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position - new Vector3(0, checkSphereYPos, 0), checkSphereRadius);
        }
    }
}