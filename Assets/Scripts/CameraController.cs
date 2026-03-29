using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panningSpeed = 10f;
    [SerializeField] int panLimitX = 5;
    [SerializeField] int panLimitY = 5;
    GridManager gridManager;
    InputActions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = Managers.Instance.gridManager;
        inputActions = new InputActions();
        inputActions.Camera.Movement.Enable();
    }

    void Update()
    {
        Vector2 vector = inputActions.Camera.Movement.ReadValue<Vector2>();
        Vector3 flattenedVector3 = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(vector.x, 0, vector.y);
        transform.position += flattenedVector3 * Time.deltaTime * panningSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -panLimitX, gridManager.width - panLimitX), transform.position.y, Mathf.Clamp(transform.position.z, -panLimitY, gridManager.height - panLimitY));

    }

}
