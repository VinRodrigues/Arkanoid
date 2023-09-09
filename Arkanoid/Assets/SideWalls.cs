using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
    public AudioClip somDeColisao;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameManager gameManager = FindObjectOfType<GameManager>(); 

        if (hitInfo.name == "Disco" && gameManager != null)
        {
            string wallName = transform.name;
            gameManager.Score(wallName); 
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);

            if (somDeColisao != null)
            {
                AudioSource audioSource = hitInfo.gameObject.GetComponent<AudioSource>();

                if (audioSource == null)
                {
                    audioSource = hitInfo.gameObject.AddComponent<AudioSource>();
                }

                audioSource.clip = somDeColisao;
                audioSource.Play();
            }
        }
        
    }
}
