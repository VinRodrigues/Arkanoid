using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameM : MonoBehaviour
{
    public int PlayerScore1 = 0;
    public int PlayerLives = 5;
    public GUISkin layout;
    private BallControl theBall; // Referência para a bola
    private AudioSource audioSource2;
    private List<obstaculo> objetosDestrutiveis = new List<obstaculo>();

    private bool gameOver = false;
    private bool levelComplete = false;
    private bool showRestartButton = false;

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
                showRestartButton = true; // Mostrar o botão de reiniciar quando o jogo terminar
            }
            else if (objetosDestrutiveis.Count == 0)
            {
                levelComplete = true;
                showRestartButton = true; // Mostrar o botão de reiniciar quando o jogo terminar
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
            customLabelStyle.fontSize = 36;
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Game Over", customLabelStyle);

            if (showRestartButton && GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "RESTART"))
            {
                RestartGame();
            }
        }
        else if (levelComplete)
        {
            GUIStyle customLabelStyle = new GUIStyle(GUI.skin.label);
            customLabelStyle.fontSize = 36;
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Fase 1 Concluída", customLabelStyle);

            if (showRestartButton && GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "RESTART"))
            {
                RestartGame();
            }
        }

        // Botão de mudar de cena
        if (GUI.Button(new Rect(Screen.width - 160, 20, 140, 53), "CHANGE SCENE"))
        {
            ChangeScene();
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
                    showRestartButton = true; // Mostrar o botão de reiniciar quando o jogo terminar
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
        showRestartButton = false;
        theBall.RestartGame();
        foreach (var obj in objetosDestrutiveis)
        {
            obj.ResetObject();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene"); // Substitua "NomeDaSuaCena" pelo nome da cena que você deseja carregar
    }
}
