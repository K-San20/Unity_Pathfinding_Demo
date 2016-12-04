using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformCost : PathFindBase
{
    public override IEnumerator FindPath(Vector3 startPos, Vector3 goalPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = m_grid.NodeFromWorldPoint(startPos);
        Node goalNode = m_grid.NodeFromWorldPoint(goalPos);

        Heap<Node> openSet = new Heap<Node>(m_grid.maxSize);
        HashSet<Node> visitedSet = new HashSet<Node>();

        startNode.m_givenCost = 0;
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currNode = openSet.RemoveFirst();
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

                int moveCost = currNode.m_givenCost + GetDistance(currNode, neighbor);
                if (moveCost < neighbor.m_givenCost || !openSet.Contains(neighbor))
                {
                    neighbor.m_givenCost = moveCost;
                    neighbor.m_parent = currNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
                else
                    openSet.UpdateItem(neighbor);
            }
        }
        yield return null;
        if (pathSuccess)
            waypoints = RetracePath(startNode, goalNode);
        m_requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }
}
