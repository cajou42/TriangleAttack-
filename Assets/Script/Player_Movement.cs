using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.ParticleSystem;

public class Player_Movement : MonoBehaviour
{
    private float m_Speed_Player = 2.3f;
    private float m_Radius;
    private float m_Angle;
    private Vector2 m_Center;
    private Rigidbody2D m_Rigidbody;
    private Game_Chronometer m_Chronometer;

    public Spawn_Obstacles m_Limit;
    public GameObject Death_Particules;
    public Canvas m_Canvas;
    public GameObject game_Over;
    public GameObject Audio;
    public int m_score = 0;
    public GameObject reward_Particle;

    private void Start()
    {
        m_Center = m_Limit.gameObject.transform.position;
        m_Radius = (m_Limit.gameObject.transform.localScale.x / 2) - transform.localScale.x / 2;
        m_Canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score : " + m_score.ToString();
        m_Rigidbody = transform.GetComponent<Rigidbody2D>();
        m_Chronometer = m_Canvas.transform.GetComponent<Game_Chronometer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            m_Angle += m_Speed_Player * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(m_Angle), Mathf.Cos(m_Angle)) * m_Radius;
            //transform.position = m_Center + offset;
            m_Rigidbody.position = m_Center + offset;
            m_Rigidbody.inertia = 2;

            transform.GetComponent<Animator>().SetBool("is_mooving", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space)) 
        {
            transform.GetComponent<Animator>().SetBool("is_mooving", false);
        }

        if(Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.y))
        {
            transform.localScale = new Vector3 (transform.localScale.y, transform.localScale.x, 0);
        }
        else if(Mathf.Abs(transform.position.y) > Mathf.Abs(transform.position.x))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Destroy_Zone")
        {
            var part = Death_Particules.transform.GetComponent<ParticleSystem>();
            part.transform.position = gameObject.transform.position;
            part.transform.rotation = gameObject.transform.rotation;
            part.Play();
            Death_Particules.transform.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            game_Over.SetActive(true);
            m_Chronometer.Reset_Speed();
            m_Limit.Reset_Last_Bonus_Count();
            m_Limit.gameObject.SetActive(false);
            m_Chronometer.Stop_Timer();
        }
        else if (collision.gameObject.tag == "Bonus")
        {
            Audio.transform.GetComponents<AudioSource>()[1].Play();
            Destroy(collision.gameObject);
            m_score++;
            m_Canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Score : \n" + m_score.ToString();
            if(m_score % 5 == 0)
            {
                reward_Particle.transform.GetComponent<ParticleSystem>().Play();
                reward_Particle.transform.GetComponent<AudioSource>().Play();
            }
        }

    }
}
