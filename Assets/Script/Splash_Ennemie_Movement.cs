using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_Ennemie_Movement : MonoBehaviour
{
    private float m_Radius;
    private Vector3 randomPoint;
    public GameObject m_Limit;
    public GameObject Explosion;

    public Canvas m_Canvas;
    private Game_Chronometer game_Chronometer;

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
    }

    void FixedUpdate()
    {
        // Make the ennemie translation to this random point
        transform.Translate(new Vector3(randomPoint.x, randomPoint.y, 0).normalized * game_Chronometer.Get_Speed() * Time.deltaTime, Space.World);
    }

    private void OnDestroy()
    {
        Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

}
