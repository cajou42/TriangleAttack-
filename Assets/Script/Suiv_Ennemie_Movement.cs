using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suiv_Ennemie_Movement : MonoBehaviour
{
    public GameObject m_Limit;
    public Canvas m_Canvas;
    private GameObject Player;
    private Game_Chronometer game_Chronometer;
    private Vector3 Fix_Player_Position;

    // Start is called before the first frame update
    void Start()
    {
        // Get the timer
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        float x = 0;
        float y = 0;

        if (Player.transform.position.x < 0 )
        {
            x = Player.transform.position.x - 2.0f;
        }
        else
        {
            x = Player.transform.position.x + 2.0f;
        }

        if (Player.transform.position.y < 0)
        {
            y = Player.transform.position.y - 2.0f;
        }
        else
        {
            y = Player.transform.position.y + 2.0f;
        }

        Fix_Player_Position = new Vector3(x, y, Player.transform.position.z);

        // Make the ennemie rotation
        Vector3 diff = Fix_Player_Position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make the ennemie translation to last player position
        transform.Translate(Fix_Player_Position.normalized * game_Chronometer.Get_Speed() * 1.70f * Time.deltaTime, Space.World);
    }
}
