//using System.Collections.Generic;
//using UnityEngine;

//public class GridManager : MonoBehaviour
//{
//    [SerializeField] GameObject tilePrefab;
//    [SerializeField] GameObject playerPrefab;
//    public int width = 5;
//    public int height = 5;

//    public Dictionary<Vector2Int, Node> grid { get; private set; }
//    public Dictionary<Vector2Int, GameObject> tiles { get; private set; }
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Awake()
//    {
//        GenerateGrid();
//    }

//    void Start()
//    {
//        GenerateTiles();
//        GeneratePlayer();

//        BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();
//        Dictionary<Node, PathNode> nodesInRange = breadthFirstSearch.GetNodes(grid[new Vector2Int(0, 0)], 3);

//        foreach (var node in nodesInRange)
//        {
//            GameObject tile = tiles[new Vector2Int((int)node.Key.position.x, (int)node.Key.position.z)];
//            tile.GetComponent<Renderer>().material.color = Color.green;
//        }

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    void OnDrawGizmos()
//    {
//        if (grid == null) return;
//        foreach (var node in grid)
//        {
//            Gizmos.DrawSphere(node.Value.position, 0.05f);
//        }
//    }

//    bool RandomWalkable()
//    {
//        bool walkable = Random.value > 0.25f;

//        return walkable;
//    }

//    void GenerateGrid()
//    {
//        grid = new Dictionary<Vector2Int, Node>();
//        for (int x = 0; x < width; x++)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                //int cost = 1;
//                int cost = Random.Range(1, 20);
//                //Node node = new Node(new Vector3Int(x, 0, y), cost, RandomWalkable());
//                Node node = new Node(new Vector3Int(x, 0, y), cost, true);
//                Vector2Int key = new Vector2Int(x, y);
//                grid[key] = node;
//            }
//        }
//    }

//    void GenerateTiles()
//    {
//        tiles = new Dictionary<Vector2Int, GameObject>();
//        GameObject tilesObject = new GameObject("Tiles");
//        foreach (var node in grid)
//        {
//            GameObject tileObject = Instantiate(tilePrefab, node.Value.position, Quaternion.identity);
//            tileObject.transform.Translate(0, -tileObject.transform.localScale.y / 2, 0);
//            tileObject.transform.parent = tilesObject.transform;
//            tiles.Add(node.Key, tileObject);

//            if (!node.Value.isWalkable) tileObject.GetComponent<Renderer>().material.color = Color.black;
//        }
//    }

//    void GeneratePlayer()
//    {
//        GameObject playerObject = Instantiate(playerPrefab, grid[new Vector2Int(0, 0)].position, Quaternion.identity);
//        Managers.Instance.playPiecesManager.playerObject = playerObject;
//    }

//}
