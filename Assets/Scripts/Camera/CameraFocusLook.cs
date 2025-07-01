using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class CameraFocusLook : MonoBehaviour
    {
       [SerializeField] private Transform target; 
       [SerializeField] private float distance = 5.0f; 
       [SerializeField] private float xSpeed = 20.0f; 
       [SerializeField] private float ySpeed = 20.0f; 

        private float x; 
        private float y;
        private InputActions inputActions;

        public void SetTarget(InteractionItem interactionItem)
        {
            target = interactionItem.transform;
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
        }

        void LateUpdate() => 
            LookAtTarget();

        public void SetInput(InputActions actions)
        {
            inputActions = actions;
            inputActions.Player.Scroll.performed += ChangeDistance;
        }

        private void LookAtTarget()
        {
            Vector2 direction = inputActions.Player.Look.ReadValue<Vector2>();
            x += direction.x * xSpeed * Time.deltaTime;
            y -= direction.y * ySpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = target.position - rotation * Vector3.forward * distance;

            transform.rotation = rotation;
            transform.position = position;
        }

        private void ChangeDistance(InputAction.CallbackContext obj)
        {
            float value = distance- obj.ReadValue<float>()/100;
            distance = Mathf.Clamp(value, 2, 10);
        }
    }
}