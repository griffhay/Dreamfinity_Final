using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIControl : MonoBehaviour {

    Canvas m_canvas;

    private void Start()
    {
        m_canvas = GetComponent<Canvas>();
    }

    public void Update () {
		if(Input.GetButtonDown("Cancel"))
        {
            if(m_canvas.enabled)
            {
                m_canvas.enabled = false;
            }
            else
            {
                m_canvas.enabled = true;
            }
        }
	}
}
