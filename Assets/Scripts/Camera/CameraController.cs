using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraMoveControll _cameraMoveControll;
        [SerializeField] private CameraFreeLook _cameraFreeLook;
        [SerializeField] private CameraFocusLook _cameraFocusLook;

        private InputActions _actions;

        private void Start()
        {
            _actions = new InputActions();
            _actions.Enable();

            _cameraMoveControll.SetInput(_actions);
            _cameraFreeLook.SetInput(_actions);
            _cameraFocusLook.SetInput(_actions);

            _actions.Player.StartLook.performed += EnableCameraControll;
            _actions.Player.StartLook.canceled += DisableCameraControll;
        }

        public void SubscribeItem(InteractionItem item)
        {
            item.OnClickEvent += FocusLook;
        }

        public void Unsubscribe(InteractionItem item)
        {
            item.OnClickEvent -= FocusLook;
        }

        private void FocusLook(InteractionItem obj)
        {
            _cameraFocusLook.enabled = true;
            _cameraFocusLook.SetTarget(obj);
        }

        private void DisableCameraControll(InputAction.CallbackContext obj)
        {
            _cameraMoveControll.enabled = false;
            _cameraFreeLook.enabled = false;
        }

        private void EnableCameraControll(InputAction.CallbackContext obj)
        {
            _cameraMoveControll.enabled = true;
            _cameraFreeLook.enabled = true;
            _cameraFocusLook.enabled = false;
        }

        private void OnDisable()
        {
            _actions.Player.StartLook.performed -= EnableCameraControll;
            _actions.Player.StartLook.canceled -= DisableCameraControll;
        }
    }
}