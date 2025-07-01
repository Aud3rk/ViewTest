using UnityEngine;

namespace Camera
{
    public class CameraFreeLook : MonoBehaviour
    {
        [SerializeField] private float sensivity;
        
        private InputActions _inputAction;

        public void SetInput(InputActions input) => 
            _inputAction = input;

        
        void Update()
        {
            Rotatation();
        }

        private void Rotatation()
        {
            Vector2 direction = _inputAction.Player.Look.ReadValue<Vector2>();
            Vector3 mouseInput = new Vector3(-direction.y, direction.x, 0);;
            transform.Rotate(mouseInput*sensivity*Time.deltaTime);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }
    }
}