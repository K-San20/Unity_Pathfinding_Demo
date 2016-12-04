using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool m_walkable;
    public Vector3 m_worldPos;
    public int m_gridX;
    public int m_gridY;
    public int m_givenCost;
    public int m_heuristicCost;
    public bool m_useFinalCost;
    public enum CostTypes { HEURISTIC = 0, GIVEN, FINAL };
    private CostTypes m_costType;
    public Node m_parent;
    int m_heapIndex;

    public int finalCost
    {
        get { return m_givenCost + m_heuristicCost; }
    }

    public int HeapIndex
    {
        get { return m_heapIndex; }
        set { m_heapIndex = value; }
    }

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY, CostTypes costType)
    {
        m_walkable = walkable;
        m_worldPos = worldPos;
        m_gridX = gridX;
        m_gridY = gridY;
        m_costType = costType;
    }

    public int CompareTo(Node toCompare)
    {
        if (toCompare == null)
            return 1;

        switch (m_costType)
        {
            case CostTypes.FINAL:
                {
                    if (toCompare != null)
                        return -(this.finalCost.CompareTo(toCompare.finalCost));
                    else
                        throw new System.ArgumentException("Object is not a Node");
                }
            case CostTypes.GIVEN:
                {
                    if (toCompare != null)
                        return -(this.m_givenCost.CompareTo(toCompare.m_givenCost));
                    else
                        throw new System.ArgumentException("Object is not a Node");
                }
            case CostTypes.HEURISTIC:
                {
                    if (toCompare != null)
                        return -(this.m_heuristicCost.CompareTo(toCompare.m_heuristicCost));
                    else
                        throw new System.ArgumentException("Object is not a Node");
                }
            default:
                {
                    if (toCompare != null)
                        return -(this.m_heuristicCost.CompareTo(toCompare.m_heuristicCost));
                    else
                        throw new System.ArgumentException("Object is not a Node");
                }
        }
    }

    public static int SortHeuristicCost(Node n1, Node n2)
    {
        return n1.m_heuristicCost.CompareTo(n2.m_heuristicCost);
    }

    public static int SortGivenCost(Node n1, Node n2)
    {
        return n1.m_givenCost.CompareTo(n2.m_givenCost);
    }
}
