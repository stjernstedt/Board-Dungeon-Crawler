using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] int moveSpeed = 1;
    GridManager gridManager;
    AStar aStar;
    List<Node> path;

    Node targetNode = null;
    Node nextNode = null;

    bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = Managers.Instance.gridManager;
        aStar = GetComponent<AStar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            FindTarget();
        }

        if (isMoving)
        {
            MoveTo(nextNode);
        }
    }

    void FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetNode = gridManager.grid[(int)hit.collider.transform.position.x, (int)hit.collider.transform.position.z];
            if (targetNode.isWalkable)
            {
                path = aStar.FindPath(gridManager.grid[(int)transform.position.x, (int)transform.position.z], targetNode);

                //gridManager.ColorTiles(path, Color.green);

                GetNextNode(path);
                isMoving = true;
            }
        }

    }

    void MoveTo(Node node)
    {
        if (transform.position == targetNode.position) { isMoving = false; return; }
        if (transform.position == node.position) { GetNextNode(path); }

        transform.position = Vector3.MoveTowards(transform.position, node.position, Time.deltaTime * moveSpeed);
    }

    public void GetNextNode(List<Node> path)
    {
        nextNode = path[0];
        path.Remove(nextNode);
    }
}
