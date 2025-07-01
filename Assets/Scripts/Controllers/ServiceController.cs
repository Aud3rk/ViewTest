using Camera;
using UI;
using UnityEngine;

public class ServiceController : MonoBehaviour
{
    [SerializeField] private SpawnerController spawnerController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UiController uiController;
    private void Start()
    {
        uiController.Init(spawnerController, cameraController);
        uiController.LoadButtons();
    }
}