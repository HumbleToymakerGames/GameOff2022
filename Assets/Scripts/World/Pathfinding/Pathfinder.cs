using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    private static Vector3Int goalGridPos;
    private static Vector3Int oldGoalGridPos;

    private static IList<Node> open = new List<Node>();
    private static IList<Node> closed = new List<Node>();

    public static IList<Vector3Int> FindPath(Vector3 start, Vector3 goal, Vector3 oldGoal, bool setAsTaken = true)
    {
        // TODO: Return list with first cell in 0 position (inverted from currnet implementation)
        open.Clear();
        closed.Clear();
        IList<Vector3Int> shortestPath = new List<Vector3Int>();

        Node[,] nodes = CreateGrid();

        Vector3Int startGridPos = MapInformation.GetTileIndex(start);
        goalGridPos = MapInformation.GetTileIndex(goal);
        oldGoalGridPos = MapInformation.GetTileIndex(oldGoal);

        if (goalGridPos.x >= MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2 || goalGridPos.y >= MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2
            || goalGridPos.x < 0 || goalGridPos.y < 0)
            return shortestPath;

        if (MapInformation.groundMap[goalGridPos.x, goalGridPos.y].taken || !MapInformation.groundMap[goalGridPos.x, goalGridPos.y].walkable)
            return shortestPath;
        else
        {
            if (oldGoalGridPos.x >= MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2 || oldGoalGridPos.y >= MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2
            || oldGoalGridPos.x < 0 || oldGoalGridPos.y < 0)
            {

            }
            else
            {
                MapInformation.groundMap[oldGoalGridPos.x, oldGoalGridPos.y].taken = false;
            }
            if(setAsTaken)
                MapInformation.groundMap[goalGridPos.x, goalGridPos.y].taken = true;
        }

        nodes[startGridPos.x, startGridPos.y].CalculateCosts(goalGridPos);
        open.Add(nodes[startGridPos.x, startGridPos.y]);

        //Will switch to a while loop but don't want to freeze game
        while (open.Count > 0 && !closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
        {
            Node currentNode = open[0];
            closed.Add(open[0]);
            open.RemoveAt(0);

            CheckNeighbors(nodes, currentNode);
        }

        if (open.Count <= 0)
        {
            shortestPath.Clear();
        }
        if (closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
        {
            shortestPath.Add(nodes[goalGridPos.x, goalGridPos.y].worldPosition);
            Node nodeTracing = nodes[goalGridPos.x, goalGridPos.y];
            while (nodeTracing != null)
            {
                shortestPath.Add(nodeTracing.worldPosition);
                nodeTracing = nodeTracing.parent;
            }
        }
        return shortestPath;
    }

    public static IList<Vector3Int> FindPath(Vector3Int start, Vector3Int goal, Vector3Int oldGoal, bool setAsTaken = true)
    {
        Debug.Log("Call to Pathfinder.FindPath");
        Debug.Log("Start: " + start.ToString() + ", Goal: " + goal.ToString() + ", Old Goal: " + oldGoal.ToString() + ", setAsTaken: " + setAsTaken.ToString());

        open.Clear();
        closed.Clear();
        IList<Vector3Int> shortestPath = new List<Vector3Int>();

        Node[,] nodes = CreateGrid();

        Vector3Int startGridPos = MapInformation.GetTileIndex(start);
        goalGridPos = MapInformation.GetTileIndex(goal);
        oldGoalGridPos = MapInformation.GetTileIndex(oldGoal);

        if (goalGridPos.x >= MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2 || goalGridPos.y >= MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2
            || goalGridPos.x < 0 || goalGridPos.y < 0)
            return shortestPath;

        if (MapInformation.groundMap[goalGridPos.x, goalGridPos.y].taken || !MapInformation.groundMap[goalGridPos.x, goalGridPos.y].walkable)
            return shortestPath;
        else
        {
            if (oldGoalGridPos.x >= MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2 || oldGoalGridPos.y >= MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2
            || oldGoalGridPos.x < 0 || oldGoalGridPos.y < 0)
            {

            }
            else
            {
                MapInformation.groundMap[oldGoalGridPos.x, oldGoalGridPos.y].taken = false;
            }
            if (setAsTaken)
                MapInformation.groundMap[goalGridPos.x, goalGridPos.y].taken = true;
        }

        nodes[startGridPos.x, startGridPos.y].CalculateCosts(goalGridPos);
        open.Add(nodes[startGridPos.x, startGridPos.y]);

        while (open.Count > 0 && !closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
        {
            Node currentNode = open[0];
            closed.Add(open[0]);
            open.RemoveAt(0);

            CheckNeighbors(nodes, currentNode);
        }

        if (open.Count <= 0)
        {
            shortestPath.Clear();
        }
        if (closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
        {
            shortestPath.Add(nodes[goalGridPos.x, goalGridPos.y].worldPosition);
            Node nodeTracing = nodes[goalGridPos.x, goalGridPos.y];
            while (nodeTracing != null)
            {
                shortestPath.Add(nodeTracing.worldPosition);
                nodeTracing = nodeTracing.parent;
            }
        }

        return shortestPath;
    }

    public static IList<Vector3Int> FindRandomPath(Vector3 start)
    {
        bool pathFound = false;
        IList<Vector3Int> shortestPath = new List<Vector3Int>();
        while (!pathFound)
        {
            open.Clear();
            closed.Clear();
            
            Node[,] nodes = CreateGrid();

            Vector3Int startGridPos = MapInformation.GetTileIndex(start);
            goalGridPos = TileSelect.SelectRandomTile().position;

            nodes[startGridPos.x, startGridPos.y].CalculateCosts(goalGridPos);
            open.Add(nodes[startGridPos.x, startGridPos.y]);

            while (open.Count > 0 && !closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
            {
                Node currentNode = open[0];
                closed.Add(open[0]);
                open.RemoveAt(0);

                CheckNeighbors(nodes, currentNode);
            }

            if (open.Count <= 0)
            {
                shortestPath.Clear();
            }
            if (closed.Contains(nodes[goalGridPos.x, goalGridPos.y]))
            {
                shortestPath.Add(nodes[goalGridPos.x, goalGridPos.y].worldPosition);
                Node nodeTracing = nodes[goalGridPos.x, goalGridPos.y];
                while (nodeTracing != null)
                {
                    shortestPath.Add(nodeTracing.worldPosition);
                    nodeTracing = nodeTracing.parent;
                }
                pathFound = true;
            }
        }
        return shortestPath;
    }

    private static void SortList(IList<Node> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Node temp;
            if (i < list.Count - 1 && list[i].fCost < list[i + 1].fCost)
            {
                temp = list[i];
                list[i] = list[i + 1];
                list[i + 1] = temp;
                if (i > 0)
                {
                    i--;
                }
            }
        }
    }

    private static void CheckNeighbors(Node[,] nodes, Node current)
    {
        if (current.gridPosition.x > 0)
        {
            CheckNodeFromCurrent(nodes, current, new Vector2Int(-1, 0));
        }
        
        if (current.gridPosition.y > 0)
        {
            CheckNodeFromCurrent(nodes, current, new Vector2Int(0, -1));
        }
        if (current.gridPosition.y < MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 1)
        {
            CheckNodeFromCurrent(nodes, current, new Vector2Int(0, 1));
        }
        if (current.gridPosition.x < MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 1)
        {
            CheckNodeFromCurrent(nodes, current, new Vector2Int(1, 0));
        }
    }

    private static void CheckNodeFromCurrent(Node[,] nodes, Node current, Vector2Int offset)
    {
        if (nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].walkable && !closed.Contains(nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y]))
        {
            if (!open.Contains(nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y]))
            {
                open.Add(nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y]);
                nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].parent = current;
                nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].CalculateCosts(goalGridPos);
            }
            else
            {
                float gCost = 0;
                Node nodeTracing = nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].parent;
                while (nodeTracing != null)
                {
                    nodeTracing = nodeTracing.parent;
                    gCost += 10;
                }


                if (gCost < nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].gCost)
                {
                    nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].parent = current;
                    nodes[current.gridPosition.x + offset.x, current.gridPosition.y + offset.y].CalculateCosts(goalGridPos);

                    SortList(open);
                }
            }
        }
    }

    private static Node[,] CreateGrid()
    {
        TileInfo tile;
        Node[,] nodes = new Node[MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2, MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2];

        for (int x = 0; x < MapInformation.groundMapBounds.max.x - MapInformation.groundMapBounds.min.x + 2; x++)
        {
            for (int y = 0; y < MapInformation.groundMapBounds.max.y - MapInformation.groundMapBounds.min.y + 2; y++)
            {
                tile = MapInformation.groundMap[x, y];
                if (tile == null)
                {
                    tile = new TileInfo(new Vector3Int(-9999, -9999, -9999), false);
                }
                nodes[x, y] = new Node(tile.position, tile.walkable);
                nodes[x, y].gridPosition = new Vector3Int(x, y, 0);
            }
        }

        return nodes;
    }
}

class Node
{
    public Node parent = null;
    public bool walkable;
    public Vector3Int worldPosition;
    public Vector3Int gridPosition;

    public bool pathChecked;

    public float fCost;
    public float gCost = 0;
    public float hCost;

    public Node(Vector3Int worldPosition, bool walkable)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
    }

    public float CalculateCosts(Vector3Int goalGridPos)
    {
        Node nodeTracing = parent;
        while (nodeTracing != null)
        {
            nodeTracing = nodeTracing.parent;
            gCost += 10;
        }
        
        hCost = Vector3Int.Distance(gridPosition, goalGridPos);

        fCost = hCost + gCost;

        return fCost;
    }
}
