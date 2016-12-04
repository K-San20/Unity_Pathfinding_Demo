using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToURL : MonoBehaviour
{
    public void VisitWebPage(string url)
    {
        Application.OpenURL(url);
    }
}
