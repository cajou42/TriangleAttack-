using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public Animator animator;

    public void Starting()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transform.GetComponent<AudioSource>().Play();
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transform.GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("Game");
    }
}
