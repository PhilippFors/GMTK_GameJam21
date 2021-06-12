using Player.PlayerInput;
using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;
        [SerializeField] private bool useLerp = true;
        [SerializeField] private float cameraFollowSpeed = 9f;
        [SerializeField] private float targetBias = 0.2f;
        
        private Camera mainCam;
        private Vector2 MousePointer => PlayerInputController.Instance.MousePointer.ReadValue();

        private void Awake()
        {
            if (objectToFollow == null)
            {
                objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
            }
            
            transform.position = objectToFollow.position;
            mainCam = Camera.main;
        }

        private void LateUpdate()
        {
            var newPos = GetCamPosition();

            if (useLerp)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * cameraFollowSpeed);
            }
            else
            {
                transform.position = newPos;
            }
        }

        private Vector3 GetCamPosition()
        {
            var pos = objectToFollow.position;

            var groundPlane = new Plane(Vector3.up, pos);

            var cameraRay = mainCam.ScreenPointToRay(MousePointer);

            if (groundPlane.Raycast(cameraRay, out var rayLength))
            {
                Vector3 rayPoint = cameraRay.GetPoint(rayLength);

                var dist = rayPoint - objectToFollow.position;

                pos += dist * targetBias;
            }

            return pos;
        }
    }
}