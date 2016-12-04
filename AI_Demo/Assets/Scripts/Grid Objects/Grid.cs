using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask m_unwalkableMask;
    public Vector2 m_gridWorldSize;
    public float m_nodeRadius;
    Node[,] m_grid;
    Vector3 worldBottomLeft;
    public float m_nodeDiameter;
    int m_gridSizeX, m_gridSizeY;
    public List<Node> m_path;
    public HashSet<Node> m_visited;
    public Node.CostTypes m_costType;

    public bool m_drawGrid;
    public bool m_drawPath;
    public bool m_drawVisited;
	
    public int maxSize { get { return m_gridSizeX * m_gridSizeY; } }

    void Awake()
    {
        m_nodeDiameter = m_nodeRadius * 2;
        m_gridSizeX = Mathf.RoundToInt(m_gridWorldSize.x / m_nodeDiameter);
        m_gridSizeY = Mathf.RoundToInt(m_gridWorldSize.y / m_nodeDiameter);
        worldBottomLeft = transform.position - Vector3.right * m_gridWorldSize.x / 2 - Vector3.forward * m_gridWorldSize.y / 2;
        CreateGrid();
    }

    void CreateGrid()
    {
        m_grid = new Node[m_gridSizeX, m_gridSizeY];

        for(int x = 0; x < m_gridSizeX; x++)
        {
            for(int y = 0; y < m_gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * m_nodeDiameter + m_nodeRadius)
                    + Vector3.forward * (y * m_nodeDiameter + m_nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, m_nodeRadius, m_unwalkableMask));
                m_grid[x, y] = new Node(walkable, worldPoint, x, y, m_costType);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        Vector3 localPos = worldPos - worldBottomLeft;

        float percentX = localPos.x / m_gridWorldSize.x;
        float percentY = localPos.z / m_gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((m_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((m_gridSizeY - 1) * percentY);
        return m_grid[x, y];

        //float percentX = (worldPos.x + m_gridWorldSize.x / 2) / m_gridWorldSize.x;
        //float percentY = (worldPos.z + m_gridWorldSize.y / 2) / m_gridWorldSize.y;
        //percentX = Mathf.Clamp01(percentX);
        //percentY = Mathf.Clamp01(percentY);

        //int x = Mathf.RoundToInt((m_gridSizeX - 1) * percentX);
        //int y = Mathf.RoundToInt((m_gridSizeY - 1) * percentY);
        //return m_grid[x, y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.m_gridX + x;
                int checkY = node.m_gridY + y;

                if (checkX >= 0 && checkX < m_gridSizeX && checkY >= 0 && checkY < m_gridSizeY)
                    neighbors.Add(m_grid[checkX, checkY]);
            }
        }

        return neighbors;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(m_gridWorldSize.x, 1, m_gridWorldSize.y));

        if (m_drawGrid && m_grid != null)
        {
            foreach (Node n in m_grid)
            {
                Gizmos.color = Color.white;
                if (m_drawVisited && m_visited.Contains(n))
                    Gizmos.color = Color.green;
                if (!n.m_walkable)
                    Gizmos.color = Color.red;
                //Gizmos.color = (n.m_walkable) ? Color.white : Color.red;
                if (m_drawPath && m_path != null)
                    if (m_path.Contains(n))
                        Gizmos.color = Color.black;
                Gizmos.DrawCube(n.m_worldPos, Vector3.one * (m_nodeDiameter - 0.1f));
            }
        }
    }
}
