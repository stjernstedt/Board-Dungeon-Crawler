using System.Collections.Generic;

public class BreadthFirstSearch
{
    GridManager gridManager = Managers.Instance.gridManager;
    public List<Node> GetNodes(Node startNode, int range)
    {
        Node[,] grid = gridManager.grid;

        Queue<Node> openList = new Queue<Node>();
        List<Node> closedList = new List<Node>();

        openList.Enqueue(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList.Dequeue();
            if (currentNode.isWalkable == false) continue;
            if (CalculateCostToNode(currentNode) > range) continue;
            foreach (var node in gridManager.GetNeighbours(currentNode))
            {
                if (!closedList.Contains(node))
                {
                    node.cameFromNode = currentNode;
                    openList.Enqueue(node);
                }
            }
            if (!closedList.Contains(currentNode))
                closedList.Add(currentNode);
        }

        return closedList;
    }

    // TODO this does not recalculate already calculated nodes, i.e. not finding the correct range for all nodes
    int CalculateCostToNode(Node node)
    {
        int totalCost = 0;
        Node cameFromNode = node.cameFromNode;
        while (cameFromNode != null)
        {
            totalCost += cameFromNode.cost;
            cameFromNode = cameFromNode.cameFromNode;
        }

        //gridManager.tiles[node.x, node.y].GetComponentInChildren<TextMeshPro>().text = totalCost.ToString();
        return totalCost;
    }
}
