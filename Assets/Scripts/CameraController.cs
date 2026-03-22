using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject camera;
    [SerializeField] float panningSpeed = 10f;
    [SerializeField] int panLimitX = 10;
    [SerializeField] int panLimitY = 10;
    GridManager gridManager;
    InputActions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = Managers.Instance.gridManager;
        inputActions = new InputActions();
        inputActions.Camera.Movement.Enable();
        //inputActions.Camera.Movement.performed += MoveCamera;
        inputActions.Camera.Rotate.Enable();
        inputActions.Camera.Rotate.performed += RotateCamera;
    }

    void Update()
    {
        Vector2 vector = inputActions.Camera.Movement.ReadValue<Vector2>();
        Vector3 flattenedVector3 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0) * new Vector3(vector.x, 0, vector.y);
        //transform.Translate(vector.x, 0, vector.y, Space.Self);
        transform.position += flattenedVector3 * Time.deltaTime * panningSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -panLimitX, gridManager.width - panLimitX), transform.position.y, Mathf.Clamp(transform.position.z, -panLimitY, gridManager.height - panLimitY));

    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        Debug.Log("Camera movement input: " + context.ReadValue<Vector2>());
    }

    private void RotateCamera(InputAction.CallbackContext context)
    {
        InputAction.CallbackContext con = context;
        Debug.Log("fired");
    }
}
