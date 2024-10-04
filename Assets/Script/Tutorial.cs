using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Panel;
    public void Display_Tuto()
    {
        Panel.SetActive(true);
    }

    public void Hide_Tuto()
    {
        Panel.SetActive(false);
    }
}
