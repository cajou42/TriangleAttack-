using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Choise : MonoBehaviour
{
    public Canvas m_Canvas;
    private Game_Chronometer game_Chronometer;
    public void Restart()
    {
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();
        game_Chronometer.Reset_Timer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Back()
    {
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();
        game_Chronometer.Reset_Timer();
        SceneManager.LoadScene("Menu");
    }
}
