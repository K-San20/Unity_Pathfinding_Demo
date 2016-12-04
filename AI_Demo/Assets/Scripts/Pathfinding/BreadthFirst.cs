using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirst : PathFindBase
{
    public override IEnumerator FindPath(Vector3 startPos, Vector3 goalPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = m_grid.NodeFromWorldPoint(startPos);
        Node goalNode = m_grid.NodeFromWorldPoint(goalPos);
        
        Queue<Node> openSet = new Queue<Node>();
        HashSet<Node> visitedSet = new HashSet<Node>();

        openSet.Enqueue(startNode);

        while(openSet.Count > 0)
        {
            Node currNode = openSet.Dequeue();
            visitedSet.Add(currNode);

            if (currNode == goalNode)
            {
                pathSuccess = true;
                //RetracePath(startNode, goalNode);
                m_grid.m_visited = visitedSet;
                break;
            }

            foreach (Node neighbor in m_grid.GetNeighbors(currNode))
            {
                if (!neighbor.m_walkable || visitedSet.Contains(neighbor))
                    continue;

                neighbor.m_parent = currNode;

                if (!openSet.Contains(neighbor))
                {
                    openSet.Enqueue(neighbor);
                    visitedSet.Add(neighbor);
                }
            }
        }
        yield return null;
        if (pathSuccess)
            waypoints = RetracePath(startNode, goalNode);
        m_requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }
}
