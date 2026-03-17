using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3Int position { get; private set; }
    public bool isWalkable { get; private set; }
    public int cost { get; private set; }
    public Node(Vector3Int position, int cost, bool isWalkable)
    {
        this.position = position;
        this.cost = cost;
        this.isWalkable = isWalkable;
    }

    public List<Node> GetNeighbours()
    {
        Dictionary<Vector2Int, Node> grid = Managers.Instance.gridManager.grid;
        List<Node> neighbours = new List<Node>();

        Node neighbour;
        if (grid.TryGetValue(new Vector2Int((int)position.x + 1, (int)position.z), out neighbour))
            neighbours.Add(neighbour);
        if (grid.TryGetValue(new Vector2Int((int)position.x - 1, (int)position.z), out neighbour))
            neighbours.Add(neighbour);
        if (grid.TryGetValue(new Vector2Int((int)position.x, (int)position.z + 1), out neighbour))
            neighbours.Add(neighbour);
        if (grid.TryGetValue(new Vector2Int((int)position.x, (int)position.z - 1), out neighbour))
            neighbours.Add(neighbour);

        return neighbours;
    }

}

public class PathNode
{
    public Node node;
    public PathNode cameFromNode;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode(Node node, PathNode cameFromNode, int gCost, int hCost)
    {
        this.node = node;
        this.cameFromNode = cameFromNode;
        this.gCost = gCost;
        this.hCost = hCost;
        fCost = gCost + hCost;
    }
}