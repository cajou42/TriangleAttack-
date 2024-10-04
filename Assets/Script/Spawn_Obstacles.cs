using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spawn_Obstacles : MonoBehaviour
{
    public GameObject Ennemie_Prefab;
    public GameObject Ennemie_Target_Prefab;
    public GameObject Ennemie_Splash_Prefab;
    public GameObject Ennemie_Suiv_Prefab;
    public GameObject Bonus;
    public GameObject Player;
    public Canvas m_Canvas;
    public GameObject Particules;
    public Animator m_Animator;

    private Game_Chronometer game_Chronometer;
    private Player_Movement Player_In_Game;
    private bool once = false;
    private float Spawn_Delay = 1.25f;
    private GameObject Current_Ennemie_Type;
    private float Last_Sec = 1.25f;
    private float Last_Sec_Bonus = 0;
    private float Last_Min = -1;
    private int Last_Bonus_Count = 0;
    private int nb_Spawn = 0;


    void Start()
    {
        game_Chronometer = m_Canvas.GetComponent<Game_Chronometer>();
        Player_In_Game = Player.GetComponent<Player_Movement>();
        transform.GetComponent<AudioSource>().Play();
        m_Animator.SetTrigger("Start_game");
    }

    private void Get_Random_Ennemie()
    {
        // Get a random ennemie
        for(int i = 0; i < nb_Spawn; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    Current_Ennemie_Type = Ennemie_Prefab;
                    break;

                case 1:
                    Current_Ennemie_Type = Ennemie_Target_Prefab;
                    break;

                case 2:
                    Current_Ennemie_Type = Ennemie_Splash_Prefab;
                    break;

                case 3:
                    Current_Ennemie_Type = Ennemie_Suiv_Prefab;
                    break;
            }
            Instantiate(Current_Ennemie_Type, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }

    void Update()
    {

        if(game_Chronometer.Get_Actual_Time_Sec() == 0 && !once)
        {
            Get_Random_Ennemie();
            once = true;

            if(nb_Spawn <= 5)
                nb_Spawn++;
            
            if(Spawn_Delay > 0.5 && game_Chronometer.Get_Actual_Time_Min() > 0.0f)
            {
                Spawn_Delay -= 0.1f;
                Last_Sec = Spawn_Delay;
                Last_Sec_Bonus = 0;
            }
        }

        if(game_Chronometer.Get_Actual_Time_Sec() == 30f && game_Chronometer.Get_Actual_Time_Min() != Last_Min)
        {
            Last_Min = game_Chronometer.Get_Actual_Time_Min();
            if (nb_Spawn < 5)
                nb_Spawn++;
        }
       
       if(game_Chronometer.Get_Actual_Time_Sec_Two_Digit() > Last_Sec) 
       {
            Last_Sec += Spawn_Delay;
            Get_Random_Ennemie();
            once = false;
       }
       
       if(game_Chronometer.Get_Actual_Time_Sec() == Last_Sec_Bonus + 7)
       {
            Instantiate(Bonus, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Last_Sec_Bonus = game_Chronometer.Get_Actual_Time_Sec();
       }

       if(Player_In_Game.m_score == Last_Bonus_Count + 5)
       {
            game_Chronometer.Increment_Speed(0.3f);
            Last_Bonus_Count = Player_In_Game.m_score;
       }
    }

    public void Reset_Last_Bonus_Count()
    {
        Last_Bonus_Count = 0;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.transform.parent.name == "Ennemie(Clone)")
        {
            Ennemie_Movement temp = other.gameObject.transform.parent.transform.GetComponent<Ennemie_Movement>();

            if(temp.m_Bounce <= 0)
            {
                Particules.transform.position = other.transform.position;
                Particules.transform.rotation = other.transform.rotation;
                MainModule value = Particules.transform.GetComponent<ParticleSystem>().main;

                var sounds = Particules.transform.GetComponents<AudioSource>()[0];
                value.startColor = new Color(220 / 255f, 71 / 255f, 0, 255);

                Particules.transform.GetComponent<ParticleSystem>().Play();
                sounds.Play();
                Destroy(other.transform.parent.gameObject);
            }

            temp.m_Bounce--;
            temp.randomPoint *= -1;
            temp.transform.GetComponent<SpriteRenderer>().color = new Color(220 / 255f, 71 / 255f, 0, 255); ;

            // Make the ennemie rotation
            Vector3 diff = temp.randomPoint - temp.transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            temp.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else if(other.gameObject.name != "Splash_Zone")
        {
            /* Particle initialisation */
            Particules.transform.position = other.transform.position;
            Particules.transform.rotation = other.transform.rotation;
            MainModule value = Particules.transform.GetComponent<ParticleSystem>().main;

            var sounds = Particules.transform.GetComponents<AudioSource>()[0];

            /* Color choice */
            switch (other.gameObject.transform.parent.name)
            {
                case "Ennemie_target(Clone)":
                    value.startColor = new Color(170/255f, 0, 255/255f, 255);
                    break;
                case "Ennemie_Splash(Clone)":
                    value.startColor = new Color(32/255f, 3/255f, 255/255f);
                    break;
                case "Ennemie_suiv(Clone)":
                    value.startColor = new Color(101, 0, 0, 255);
                    break;
                case "Point(Clone)":
                    value.startColor = Color.green;
                    break;
            }
            Particules.transform.GetComponent<ParticleSystem>().Play();
            sounds.Play();
            Destroy(other.transform.parent.gameObject);
        }
    }
}
