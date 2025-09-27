namespace FpsHorrorKit
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Unity.Cinemachine;

    [RequireComponent(typeof(CharacterController))]
    public class FpsController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 4.0f;
        public float accelerationRate = 10.0f;
        public float decelerationRate = 10f;

        [Header("Camera Settings")]
        public CinemachineCamera virtualCamera;
        public float maxCameraPitch = 70f;
        public float minCameraPitch = -70f;
        public float rotationSpeed = 1.0f;

        [Header("Headbob Settings")]
        public CinemachineBasicMultiChannelPerlin headBob;
        public float headBobAcceleration = 10f;
        public float idleBobAmp = .5f;
        public float idleBobFreq = 1f;
        public float walkBobAmp = 3f;
        public float walkBobFreq = 1f;

        [Header("Interact Settings")]
        public bool isInteracting = false;

        private CharacterController characterController;
        private FpsAssetsInputs _input;

        private Vector3 velocity;
        private float cameraPitch;

        public float CameraPich { get { return cameraPitch; } set { cameraPitch = value; } }
        public Vector3 Velocity { get { return velocity; } set { velocity = value; } }

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            var playerInput = GetComponent<PlayerInput>();
            _input = GetComponent<FpsAssetsInputs>();
        }

        private void Start()
        {
            if (virtualCamera == null)
            {
                Debug.LogError("Cinemachine Virtual Camera is not assigned.");
            }
        }

        private void Update()
        {
            HandleMovement();
        }
        private void LateUpdate()
        {
            HandleRotation();
        }
        private void HandleMovement()
        {
            if (isInteracting)
            {
                _input.move = Vector2.zero;
                velocity = Vector3.zero;

                headBob.AmplitudeGain = idleBobAmp;
                headBob.FrequencyGain = idleBobFreq;
                return;
            }

            HeadBob();

            Vector2 input = _input.move;
            Vector3 moveDirection = transform.right * input.x + transform.forward * input.y;

            if (moveDirection != Vector3.zero)
            {
                velocity.x = Mathf.Lerp(velocity.x, walkSpeed * moveDirection.x, Time.deltaTime * accelerationRate);
                velocity.z = Mathf.Lerp(velocity.z, walkSpeed * moveDirection.z, Time.deltaTime * accelerationRate);
            }
            else
            {
                velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime * decelerationRate);
                velocity.z = Mathf.Lerp(velocity.z, 0, Time.deltaTime * decelerationRate);
            }

            characterController.Move(new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (isInteracting) { return; }

            Vector2 lookInput = _input.look;
            cameraPitch += lookInput.y * rotationSpeed;
            cameraPitch = Mathf.Clamp(cameraPitch, minCameraPitch, maxCameraPitch);

            virtualCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
            transform.Rotate(Vector3.up * lookInput.x * rotationSpeed);
        }

        private void HeadBob()
        {
            float moveMagnitude = _input.move.magnitude; // Hareket miktarını hesapla
            float targetAmp = moveMagnitude > 0 ? walkBobAmp : idleBobAmp;
            float targetFreq = moveMagnitude > 0 ? walkBobFreq : idleBobFreq;

            headBob.AmplitudeGain = Mathf.Lerp(headBob.AmplitudeGain, targetAmp, Time.deltaTime * headBobAcceleration);
            headBob.FrequencyGain = Mathf.Lerp(headBob.FrequencyGain, targetFreq, Time.deltaTime * headBobAcceleration);
        }
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            
        }
    }
}