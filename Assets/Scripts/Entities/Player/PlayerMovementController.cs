using System.Collections;
using UnityEngine;
using Entities.Player.PlayerInput;

namespace Entities.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        public Vector3 CurrentVelocity { get; private set; }

        [SerializeField] private float mainMovementSpeed;
        
        [Header("Dash")]
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashRechargeTime;
        [SerializeField] private float dashDuration;
        
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
        private CharacterController characterController;
        private Vector3 oldPos;
        private float dashCharge;
        private float dashTime;
        private float maxDashCharge = 100f;
        private float currentMovementSpeed;
        
        private void Awake()
        {
            currentMovementSpeed = mainMovementSpeed;
            oldPos = transform.position;
            mainCam = Camera.main;
            characterController = GetComponent<CharacterController>();
            PlayerInputController.Instance.Dash.Performed += ctx => StartDash();
            PlayerInputController.Instance.Movement.Performed += ctx => GetComponentInChildren<Animator>().SetBool("isRunning", true);
            
        }
        
        private void Update()
        {
            IsGrounded();
            Move();
            UpdateLookDirection();
            DashCooldown();
            CurrentVelocity = transform.position - oldPos;
            if (PlayerInputController.Instance.Movement.ReadValue() == Vector2.zero)
            {
                GetComponentInChildren<Animator>().SetBool("isRunning", false);
            }
        }

        private void DashCooldown()
        {
            float timeSinceDashEnded = Time.time - dashTime;

            float perc = timeSinceDashEnded / dashRechargeTime;

            dashCharge = Mathf.Lerp(0, maxDashCharge, perc);
        }

        private void StartDash()
        {
            if (dashCharge >= 100)
            {
                isDashing = true;
                
                dashCharge = 0;
                
                currentMovementSpeed = dashSpeed;

                StartCoroutine(DashTimer());
            }
        }

        private IEnumerator DashTimer()
        {
            yield return new WaitForSeconds(dashDuration);

            ResetMovement();
        }
        
        private void ResetMovement()
        {
            dashTime = Time.time;
            currentMovementSpeed = mainMovementSpeed;
            isDashing = false;
        }
        
        private void Move()
        {
            characterController.Move((currentMoveDirection + velocity).normalized * (currentMovementSpeed * Time.deltaTime));

            var move = MovementDir;
            var direction = new Vector3(move.x, 0, move.y);

            if (!isDashing)
            {
                currentMoveDirection = direction * currentMovementSpeed;
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