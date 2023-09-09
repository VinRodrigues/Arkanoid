using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int PlayerScore1 = 0;
    public int PlayerLives = 5;
    public GUISkin layout;
    private BallControl theBall; // Referência para a bola
    private AudioSource audioSource2;
    private List<obstaculo> objetosDestrutiveis = new List<obstaculo>();

    private bool gameOver = false;
    private bool levelComplete = false;

    private void Start()
    {
        theBall = FindObjectOfType<BallControl>(); // Encontre a bola na cena
        audioSource2 = GetComponent<AudioSource>();

        objetosDestrutiveis.AddRange(FindObjectsOfType<obstaculo>());
    }

    private void Update()
    {
        if (!gameOver && !levelComplete)
        {
            if (PlayerLives <= 0)
            {
                gameOver = true;
            }
            else if (objetosDestrutiveis.Count == 0)
            {
                levelComplete = true;
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "Score: " + PlayerScore1);
        GUI.Label(new Rect(20, 20, 100, 100), "Lives: " + PlayerLives);

        if (gameOver)
        {
            GUIStyle customLabelStyle = new GUIStyle(GUI.skin.label);
            customLabelStyle.fontSize = 60;
          
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Game Over", customLabelStyle);
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "RESTART"))
            {
                RestartGame();
            }
        }
        else if (levelComplete)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Fase 1 Concluída");
        }
    }

    public void Score(string wallID)
    {
        if (!gameOver && !levelComplete)
        {
            if (wallID == "Bottomwall")
            {
                PlayerLives--; // Perde uma vida
                if (PlayerLives <= 0)
                {
                    gameOver = true;
                }
                else
                {
                    theBall.RestartGame(); // Chame o método para reiniciar a bola
                }
            }
            else
            {
                PlayerScore1 += 100;
                audioSource2.Play();
            }
        }
    }

    public void RestartGame()
    {
        PlayerScore1 = 0;
        PlayerLives = 5;
        gameOver = false;
        levelComplete = false;

        foreach (var obj in objetosDestrutiveis)
        {
            obj.ResetObject();
        }
    }
}
