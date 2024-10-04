using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Chronometer : MonoBehaviour
{
    public static float Actual_Time_Min = 0.0f;
    public static float Actual_Time_Sec = 0.0f;
    public static float Ennemie_Speed = 4.2f;
    public static float Target_Ennemie_Speed = 2.0f;
    public TextMeshProUGUI textbox;
    private float Last_Time_State = 0f;
    private bool once = true;
    private bool m_stop = false;


    void Update()
    {
        if (!m_stop)
        {
            textbox.text = Mathf.Floor(Actual_Time_Min).ToString() + ":" + Mathf.Floor(Actual_Time_Sec).ToString();
            Actual_Time_Sec += Time.deltaTime;

            if (Actual_Time_Sec >= 60.0f)
            {
                Actual_Time_Sec = 0;
                ++Actual_Time_Min;
            }

            // Increment ennemie speed
            if (Mathf.Floor(Actual_Time_Sec) == 0 && !once)
            {
                Ennemie_Speed += 0.2f;
                Last_Time_State = 0;
                once = true;
            }

            if (Mathf.Floor(Actual_Time_Sec) == Last_Time_State + 10)
            {
                Ennemie_Speed += 0.2f;
                Last_Time_State = Mathf.Floor(Actual_Time_Sec);
                once = false;
            }
        }

    }

    public float Get_Actual_Time_Min()
    {
        return Mathf.Floor(Actual_Time_Min);
    }
    public float Get_Actual_Time_Sec()
    {
        return Mathf.Floor(Actual_Time_Sec);
    }

    public float Get_Actual_Time_Sec_Two_Digit()
    {
        return Mathf.Floor(Actual_Time_Sec * 100) * 0.01f;
    }

    public float Get_Speed()
    {
        return Ennemie_Speed;
    }

    public float Increment_Speed(float increment)
    {
        return Ennemie_Speed += increment;
    }

    public float Reset_Speed()
    {
        return Ennemie_Speed = 4.5f;
    }

    public float Get_Target_Speed()
    {
        return Target_Ennemie_Speed;
    }

    public void Reset_Timer()
    {
        Actual_Time_Min = 0.0f;
        Actual_Time_Sec = 0.0f;
        m_stop = false;
    }

    public void Stop_Timer()
    {
        m_stop = true;
    }
}
