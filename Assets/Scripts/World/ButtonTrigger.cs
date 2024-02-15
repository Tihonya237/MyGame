using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTrigger : MonoBehaviour
{
    public Animator anim;
    public GameObject h1,h2,h3;
    public GameObject[] otherScenes;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetTrigger("IsTriggered");

            if(collision.CompareTag("house1"))
            {
                h1.SetActive(true);
            

            }
            if (collision.CompareTag("house2"))
            {
                h2.SetActive(true);

               
            }


        }
    }


   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("IsTriggered");
            if (collision.CompareTag("house1"))
            {
                h1.SetActive(false);
            }
            if (collision.CompareTag("house2"))
            {
                h2.SetActive(false);
            }
        }
    }
}


