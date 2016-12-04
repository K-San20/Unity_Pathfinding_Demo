using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> m_pathRequestQueue = new Queue<PathRequest>();
    PathRequest m_currPathRequest;

    static PathRequestManager m_instance;
    PathFindBase m_pathFinding;

    bool m_isProcessingPath;

    void Awake()
    {
        m_instance = this;
        m_pathFinding = GetComponent<PathFindBase>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        m_instance.m_pathRequestQueue.Enqueue(newRequest);
        m_instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if(!m_isProcessingPath && m_pathRequestQueue.Count > 0)
        {
            m_currPathRequest = m_pathRequestQueue.Dequeue();
            m_isProcessingPath = true;
            m_pathFinding.StartFindPath(m_currPathRequest.m_pathStart, m_currPathRequest.m_pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        m_currPathRequest.m_callback(path, success);
        m_isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 m_pathStart;
        public Vector3 m_pathEnd;
        public Action<Vector3[], bool> m_callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            m_pathStart = start;
            m_pathEnd = end;
            m_callback = callback;
        }
    }
	
}
