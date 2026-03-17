using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject camera;
    [SerializeField] float panningSpeed = 10f;
    InputActions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputActions();
        inputActions.Camera.Movement.Enable();
        //inputActions.Camera.Movement.performed += MoveCamera;
    }

    void Update()
    {
        Vector2 vector = inputActions.Camera.Movement.ReadValue<Vector2>();
        Vector3 flattenedVector3 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * new Vector3(vector.x, 0, vector.y);
        //transform.Translate(vector.x, 0, vector.y, Space.Self);
        transform.position += flattenedVector3 * Time.deltaTime * panningSpeed;

    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        Debug.Log("Camera movement input: " + context.ReadValue<Vector2>());
    }
}
