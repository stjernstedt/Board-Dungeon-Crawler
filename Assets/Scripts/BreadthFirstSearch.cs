using System.Collections.Generic;

public class BreadthFirstSearch
{
    public Dictionary<Node, PathNode> GetNodes(Node startNode, int range)
    {
        Queue<PathNode> openList = new Queue<PathNode>();
        Dictionary<Node, PathNode> closedList = new Dictionary<Node, PathNode>();

        PathNode startPathNode = new PathNode(startNode, null, 0, 0);
        openList.Enqueue(startPathNode);

        while (openList.Count > 0)
        {
            PathNode currentPathNode = openList.Dequeue();
            if (CalculateCostToNode(currentPathNode) > range) continue;
            if (currentPathNode.node.isWalkable == false) continue;
            foreach (var node in currentPathNode.node.GetNeighbours())
            {
                if (!closedList.ContainsKey(node))
                    openList.Enqueue(new PathNode(node, currentPathNode, 0, 0));
            }
            if (!closedList.ContainsKey(currentPathNode.node))
                closedList.Add(currentPathNode.node, currentPathNode.cameFromNode);
        }

        return closedList;
    }

    int CalculateCostToNode(PathNode node)
    {
        int totalCost = 0;
        PathNode cameFromNode = node.cameFromNode;
        while (cameFromNode != null)
        {
            totalCost += cameFromNode.node.cost;
            cameFromNode = cameFromNode.cameFromNode;
        }

        return totalCost;
    }
}
