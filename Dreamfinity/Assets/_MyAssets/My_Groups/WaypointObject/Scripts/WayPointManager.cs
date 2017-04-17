using UnityEngine;

[ExecuteInEditMode]



public class WayPointManager : MonoBehaviour {

    public bool m_childRender;


    public void RenderChilderen()
    {
        if (Application.isPlaying)
        {
            m_childRender = true;
        }

        if (!m_childRender)
        {
            transform.GetComponentInChildren<WaypointControl>().m_toggle = true;
            m_childRender = true;
        }
        else if (m_childRender)
        {
            transform.GetComponentInChildren<WaypointControl>().m_toggle = false;
            m_childRender = false;
        }
    }
}

/*
 //public GameObject[] m_wayPoints;
    //Vector3[] m_wayPointPos;
    //public int m_indexSize;

    //void Awake()
    //{
    //    Debug.Log("Editor causes this Awake");

    //    runInEditMode = true;

    //    m_indexSize = transform.childCount;

    //    m_wayPoints = new GameObject[m_indexSize];
    //    m_wayPointPos = new Vector3[m_indexSize +1];

    //}

    //void Update()
    //{
    //    if (transform.childCount != m_indexSize)
    //    {

    //        m_indexSize = transform.childCount;

    //        ResizeArray();

    //    }

    //   // RenderLines();
    //}

    ////public void RenderLines()
    ////{
    ////    LineRenderer lineRend = GetComponent<LineRenderer>();

    ////    ResizeArray();

    ////    lineRend.numPositions = m_indexSize+1;

    ////    lineRend.SetPositions(m_wayPointPos);
    ////}

    //private void ResizeArray()
    //{
    //    m_wayPoints = new GameObject[m_indexSize];
    //    m_wayPointPos = new Vector3[m_indexSize +1];

    //    for (int i = 0; i < m_indexSize; i++)
    //    {
    //        m_wayPoints[i] = transform.GetChild(i).gameObject;
    //        m_wayPointPos[i] = m_wayPoints[i].transform.position;

    //        if(i == m_indexSize - 1)
    //        {
    //            m_wayPointPos[i + 1] = m_wayPointPos[0];
    //        }
    //    }

        
       
    //}
     */
