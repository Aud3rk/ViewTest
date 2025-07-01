using UnityEngine;

namespace Camera
{
    public class CameraMoveControll : MonoBehaviour
    {
        [SerializeField] private float currentSpeed;
    
        private InputActions _inputAction;
        private Vector3 _lookDirection;

        public void SetInput(InputActions input) => 
            _inputAction = input;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var moveDirection = _inputAction.Player.Move.ReadValue<Vector2>();
            Vector3 input = new Vector3(moveDirection.x, 0f, moveDirection.y);
            transform.Translate(input*currentSpeed*Time.deltaTime);
        }
    }
}
