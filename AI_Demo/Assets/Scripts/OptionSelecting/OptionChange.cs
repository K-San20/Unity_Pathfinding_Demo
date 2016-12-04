using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionChange : MonoBehaviour
{
    public UnityEngine.UI.Button m_gridButton;
    public UnityEngine.UI.Button m_pathButton;
    public UnityEngine.UI.Button m_visitedButton;
    public UnityEngine.UI.Button m_musicButton;
    public Grid m_grid;
    bool musicOn;
    public GameObject bgMusic;
    public GameObject ai;

    public void ToggleGrid()
    {
        if (m_grid.m_drawGrid)
        {
            m_grid.m_drawGrid = false;
            m_gridButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
        }
        else
        {
            m_grid.m_drawGrid = true;
            m_gridButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.green;
        }
    }

    public void TogglePath()
    {
        if (m_grid.m_drawPath)
        {
            m_grid.m_drawPath = false;
            m_pathButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
        }
        else
        {
            m_grid.m_drawPath = true;
            m_pathButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.green;
        }
    }

    public void ToggleVisited()
    {
        if (m_grid.m_drawVisited)
        {
            m_grid.m_drawVisited = false;
            m_visitedButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
        }
        else
        {
            m_grid.m_drawVisited = true;
            m_visitedButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.green;
        }
    }

    public void ToggleMusic()
    {
        if(musicOn)
        {
            musicOn = false;
            m_musicButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.red;
            bgMusic.GetComponent<AudioSource>().volume = 0.0f;
        }
        else
        {
            musicOn = true;
            m_musicButton.GetComponentInChildren<UnityEngine.UI.Text>().color = Color.green;
            bgMusic.GetComponent<AudioSource>().volume = 1.0f;
        }
    }

    public void SetAISpeed()
    {
        ai.GetComponent<AIControl>().m_speed = GameObject.Find("AISlider").GetComponent<UnityEngine.UI.Slider>().value;
    }
}
