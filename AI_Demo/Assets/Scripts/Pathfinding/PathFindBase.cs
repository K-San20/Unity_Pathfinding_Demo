using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindBase : MonoBehaviour
{
    protected PathRequestManager m_requestManager;
    protected Grid m_grid;
    //public Transform seeker, target;

    void Awake()
    {
        m_requestManager = GetComponent<PathRequestManager>();
        m_grid = GetComponent<Grid>();
    }

    //void Update()
    //{
    //    FindPath(seeker.position, target.position);
    //}

    public void StartFindPath(Vector3 startPos, Vector3 goalPos)
    {
        StartCoroutine(FindPath(startPos, goalPos));
    }

    public virtual IEnumerator FindPath(Vector3 startPos, Vector3 goalPos) { yield return null; }

    protected Vector3[] RetracePath(Node startNode, Node goalNode)
    {
        List<Node> path = new List<Node>();
        Node currNode = goalNode;

        while (currNode != startNode)
        {
            path.Add(currNode);
            currNode = currNode.m_parent;
        }

        Vector3[] waypoints = SimplifyPath(path);
        System.Array.Reverse(waypoints);

        path.Reverse();
        m_grid.m_path = path;

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDir = Vector2.zero;
        //waypoints.Add(path[0].m_worldPos);

        for(int i = 0; i < path.Count-1; i++)
        {
            Vector2 newDir = new Vector2(path[i + 1].m_gridX - path[i].m_gridX, path[i + 1].m_gridY - path[i].m_gridY);
            if (newDir != oldDir)
                waypoints.Add(path[i].m_worldPos);
            oldDir = newDir;
        }

        return waypoints.ToArray();
    }

    //protected void RetracePath(Node startNode, Node goalNode)
    //{
    //    List<Node> path = new List<Node>();
    //    Node currNode = goalNode;

    //    while (currNode != startNode)
    //    {
    //        path.Add(currNode);
    //        currNode = currNode.m_parent;
    //    }

    //    path.Reverse();
    //    m_grid.m_path = path;
    //}

    protected int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.m_gridX - nodeB.m_gridX);
        int distY = Mathf.Abs(nodeA.m_gridY - nodeB.m_gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);

    }
}
