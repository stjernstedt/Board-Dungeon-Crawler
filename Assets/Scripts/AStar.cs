using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AStar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<PathNode> path = GetPath(Managers.Instance.gridManager.grid[new Vector2Int(0, 0)], Managers.Instance.gridManager.grid[new Vector2Int(4, 8)]);
        InvokeRepeating(nameof(FindTarget), 0, 0.1f);
    }

    void FindTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Node node = Managers.Instance.gridManager.grid[new Vector2Int((int)hit.collider.transform.position.x, (int)hit.collider.transform.position.z)];
            GetPath(Managers.Instance.gridManager.grid[new Vector2Int(0, 0)], node);
        }

    }

    public List<PathNode> GetPath(Node start, Node target)
    {
        PriorityQueue<PathNode> openList = new PriorityQueue<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        List<PathNode> path = new List<PathNode>();

        PathNode startingNode = new PathNode(start, null, 0, CalculateHCost(start, target));
        openList.Enqueue(startingNode, startingNode.fCost);
        PathNode currentNode;

        while (openList.Count > 0)
        {
            currentNode = openList.Dequeue();

            if (currentNode.node == target)
            {
                while (currentNode != null)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.cameFromNode;
                }

                //////////////////////
                foreach (var node in Managers.Instance.gridManager.grid)
                {
                    if (node.Value.isWalkable == false) continue;
                    Managers.Instance.gridManager.tiles[node.Key].GetComponent<Renderer>().material.color = Color.white;
                }
                ColorTiles(closedList, Color.blue);
                ColorTiles(path, Color.yellow);
                /////////////////////
                return path;
            }

            closedList.Add(currentNode);

            foreach (var node in currentNode.node.GetNeighbours())
            {
                if (node.isWalkable == false) continue;
                bool inClosedList = false;
                foreach (var pathnode in closedList)
                {
                    if (pathnode.node == node) inClosedList = true;
                }
                if (inClosedList) continue;

                bool inOpenList = false;

                foreach (var pathNode in openList.ToList())
                {
                    if (pathNode.node == node) inOpenList = true;
                }
                if (!inOpenList)
                {
                    PathNode neighbour = new PathNode(node, currentNode, CalculateGCost(currentNode, node), CalculateHCost(node, target));
                    openList.Enqueue(neighbour, neighbour.fCost);
                    continue;
                }

                int tmpGCost = CalculateGCost(currentNode, node);
                foreach (var pathNode in openList.ToList())
                {
                    // this code only fires on unwalkable nodes, and only sometimes?
                    if (pathNode.node == node && tmpGCost < pathNode.gCost)
                    {
                        Debug.Log("updated node");
                        pathNode.cameFromNode = currentNode;
                        pathNode.gCost = tmpGCost;
                        pathNode.fCost = pathNode.gCost + pathNode.hCost;
                        openList.UpdatePriority(pathNode, pathNode.fCost);
                    }
                }

            }
        }
        return null;
    }

    int CalculateGCost(PathNode node, Node nextNode)
    {
        return node.gCost + nextNode.cost;
    }

    int CalculateHCost(Node node, Node targetNode)
    {
        return (int)Vector3Int.Distance(node.position, targetNode.position);
    }

    public void ColorTiles(List<PathNode> path, Color color)
    {
        foreach (var pathnode in path)
        {
            GameObject tile = Managers.Instance.gridManager.tiles[new Vector2Int(pathnode.node.position.x, pathnode.node.position.z)];
            tile.GetComponent<Renderer>().material.color = color;
        }
    }
}