using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls2 : MonoBehaviour
{
    public AudioClip somDeColisao;

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        GameM gameManager2 = FindObjectOfType<GameM>();

        if (hitInfo.name == "Ball" && gameManager2 != null)
        {
            string wallName = transform.name;
            gameManager2.Score(wallName);
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
