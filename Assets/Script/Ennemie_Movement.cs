using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Ennemie_Movement : MonoBehaviour
{

    private float m_Radius;
    private Game_Chronometer game_Chronometer;

    public Vector3 randomPoint;
    public GameObject m_Limit;
    public Canvas m_Canvas;
    public int m_Bounce;
    

    private void Start()
    {
        // Get the timer
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();

        // Define a random point
        m_Radius = m_Limit.transform.localScale.x;
        randomPoint = m_Limit.transform.position + (Vector3)Random.insideUnitCircle * m_Radius;

        // Make the ennemie rotation
        Vector3 diff = randomPoint - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        m_Bounce = 1;
    }

    void FixedUpdate()
    {
        // Make the ennemie translation to this random point
        transform.Translate(new Vector3(randomPoint.x, randomPoint.y, 0).normalized * game_Chronometer.Get_Speed() * Time.deltaTime, Space.World);
    }

}
