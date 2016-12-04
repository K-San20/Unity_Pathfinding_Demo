using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public Transform m_target;
    public float m_speed;
    Vector3[] m_path;
    int m_targetIndex;
    Vector3 oldPos;
    bool stopped = false;

    void Start()
    {
        oldPos = m_target.position;
        PathRequestManager.RequestPath(transform.position, m_target.position, OnPathFound);
    }

    void Update()
    {
        if(oldPos != m_target.position)
        {
            oldPos = m_target.position;
            PathRequestManager.RequestPath(transform.position, m_target.position, OnPathFound);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            m_path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (m_path.Length != 0)
        {
            Vector3 currWaypoint = m_path[0];

            while (true)
            {
                if (transform.position == currWaypoint)
                {
                    m_targetIndex++;
                    if (m_targetIndex >= m_path.Length)
                    {
                        m_targetIndex = 0;
                        m_path = new Vector3[0];
                        yield break;
                    }
                    currWaypoint = m_path[m_targetIndex];
                }

                if (!stopped)
                    transform.position = Vector3.MoveTowards(transform.position, currWaypoint, m_speed);
                yield return null;
            }
        }

    }
}
