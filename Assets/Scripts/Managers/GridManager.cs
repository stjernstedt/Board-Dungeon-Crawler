using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Node[,] grid { get; private set; }
    public GameObject[,] tiles { get; private set; }
    public GameObject tilePrefab;
    public int width, height;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        grid = new Node[width, height];
        GenerateGrid();
        GenerateTiles();

    }

    // Update is called once per frame
    void Start()
    {
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int cost = 1;
                //bool isWalkable = true;
                bool isWalkable = RandomWalkable();
                grid[x, y] = new Node(new Vector3Int(x, 0, y), x, y, cost, isWalkable);
            }
        }
    }

    void GenerateTiles()
    {
        tiles = new GameObject[width, height];
        GameObject tilesObject = new GameObject("Tiles");
        foreach (var node in grid)
        {
            GameObject tileObject = Instantiate(tilePrefab, node.position, Quaternion.identity);
            tileObject.transform.Translate(0, -tileObject.transform.localScale.y / 2, 0);
            tileObject.transform.parent = tilesObject.transform;
            tiles[node.position.x, node.position.z] = tileObject;

            if (!node.isWalkable) tileObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    void OnDrawGizmos()
    {
        if (grid == null) return;
        foreach (var node in grid)
        {
            Gizmos.DrawSphere(node.position, 0.05f);
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        if (node.x > 0)
            neighbours.Add(grid[node.x - 1, node.y]);
        if (node.x < grid.GetLength(0) - 1)
            neighbours.Add(grid[node.x + 1, node.y]);
        if (node.y > 0)
            neighbours.Add(grid[node.x, node.y - 1]);
        if (node.y < grid.GetLength(1) - 1)
            neighbours.Add(grid[node.x, node.y + 1]);

        return neighbours;
    }


    bool RandomWalkable()
    {
        bool isWalkable = Random.value > 0.3;
        return isWalkable;
    }

}