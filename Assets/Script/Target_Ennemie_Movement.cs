using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Ennemie_Movement : MonoBehaviour
{
    public GameObject m_Limit;
    public Canvas m_Canvas;
    private GameObject Player;
    private Game_Chronometer game_Chronometer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the timer
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Make the ennemie translation to last player position
        transform.Translate(Player.transform.position.normalized * game_Chronometer.Get_Target_Speed() * Time.deltaTime, Space.World);

        // Make the ennemie rotation
        Vector3 diff = Player.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
