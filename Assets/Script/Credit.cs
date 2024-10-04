using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject Panel;
    public void Display_Credit()
    {
        Panel.SetActive(true);
    }

    public void Hide_Credit()
    {
        Panel.SetActive(false);
    }
}
