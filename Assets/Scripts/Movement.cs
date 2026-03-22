using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] int moveSpeed = 1;
    [SerializeField] GridManager gridManager;
    GameObject playerObject;

    Node node = null;

    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = Managers.Instance.playPiecesManager.playerObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveTo(node);
        }
    }

    void MoveTo(Node node)
    {
        if (playerObject.transform.position == node.position) { isMoving = false; return; }

        playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, node.position, Time.deltaTime * moveSpeed);
    }

    public void GetNode(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //node = gridManager.grid[new Vector2Int((int)hit.collider.transform.position.x, (int)hit.collider.transform.position.z)];
            }

            isMoving = true;
        }
    }
}
