using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class AStar : MonoBehaviour
{
    Node[,] grid;
    GridManager gridManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = Managers.Instance.gridManager;
        grid = gridManager.grid;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    FindTarget();
        //}
    }

    public void FindTarget()
    {
        GameObject[,] tiles = Managers.Instance.gridManager.tiles;
        foreach (var node in grid)
        {
            if (node.isWalkable)
                tiles[node.x, node.y].GetComponent<Renderer>().material.color = Color.white;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Node node = grid[(int)hit.collider.transform.position.x, (int)hit.collider.transform.position.z];
            if (node.isWalkable)
            {
                List<Node> path = FindPath(grid[0, 0], node);

                gridManager.ColorTiles(path, Color.green);
            }
        }
    }

    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        startNode.cameFromNode = null;

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                break;
            }

            foreach (var neighbour in gridManager.GetNeighbours(currentNode))
            {
                if (neighbour.isWalkable == false || closedList.Contains(neighbour))
                {
                    continue;
                }

                //int tentativeGCost = currentNode.gCost + ManhattanDistance(currentNode, neighbour); // make it not manhattan distance, but the actual cost to move from currentNode to neighbour
                int tentativeGCost = currentNode.gCost + neighbour.cost;
                if (tentativeGCost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = ManhattanDistance(neighbour, targetNode);
                    neighbour.cameFromNode = currentNode;

                    if (!openList.Contains(neighbour)) openList.Add(neighbour);
                }
            }
        }

        gridManager.ColorTiles(closedList.ToList<Node>(), Color.blue);
        List<Node> path = RetracePath(startNode, targetNode);
        if (path == null) return null;
        return path;
    }

    List<Node> RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }

    int ManhattanDistance(Node node, Node targetNode)
    {
        return (int)(Mathf.Abs(node.position.x - targetNode.position.x) + Mathf.Abs(node.position.z - targetNode.position.z));
    }

}
