using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int PlayerScore1 = 0;
    public int PlayerLives = 5;
    public GUISkin layout;
    private BallControl theBall; 
    private AudioSource audioSource2;
    private List<obstaculo> objetosDestrutiveis = new List<obstaculo>();

    private bool gameOver = false;
    private bool levelComplete = false;
    private bool showRestartButton = false;
    private bool showCongratsMessage = false;
    private bool showMainMenuButton = false;
    private int objetosDestruidos = 0; 

    private void Start()
    {
        theBall = FindObjectOfType<BallControl>(); 
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
                showRestartButton = true; // Mostra o botão de reiniciar quando o jogo terminar
            }
            else if (SceneManager.GetActiveScene().name == "SampleScene" && PlayerScore1 >= 3000) // Verifica se 30000 pontos foram alcançados na "SampleScene".
            {
                levelComplete = true;
                showRestartButton = true; // Mostra o botão de reiniciar quando o jogo terminar
            }
            else if (SceneManager.GetActiveScene().name == "fase2" && PlayerScore1 >= 6000) // Verifica se 6000 pontos foram alcançados na "fase2".
            {
                levelComplete = true;
                 
                showCongratsMessage = true; // Ativa a mensagem de parabéns
                showMainMenuButton = true;
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 800 - 12, 20, 100, 100), "Lives: " + PlayerLives);  // para vizuliar na tela do unity use 400 inves de 800
        GUI.Label(new Rect(Screen.width / 2 - 800 - 12, 50, 100, 100), "Score: " + PlayerScore1);//para vizuliar na tela do unity use 400 inves de 800

        if (!gameOver && !levelComplete)
        {
            // Botão para reiniciar a bola
            if (GUI.Button(new Rect(Screen.width / 2 - 800, 420, 120, 53), "Reset Bola"))//para vizuliar na tela do unity use 450 inves de 800
            {
                ResetBall();
            }
        }

        if (gameOver)
        {
            GUIStyle customLabelStyle = new GUIStyle(GUI.skin.label);
            customLabelStyle.fontSize = 30;
            customLabelStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 85, Screen.height / 2 - 50, 200, 100), "Game Over", customLabelStyle);

            if (showRestartButton && GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "RESTART"))
            {
                RestartGame();
            }
        }
        else if (levelComplete)
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                GUIStyle customLabelStyle = new GUIStyle(GUI.skin.label);
                customLabelStyle.fontSize = 36;
                customLabelStyle.normal.textColor = Color.yellow;
                GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "Fase 1 Concluída", customLabelStyle);

                if (showRestartButton && GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "Fase 2"))
                {
                    ChangeScene();
                }
            }
            else if (SceneManager.GetActiveScene().name == "fase2")
            {
                if (showCongratsMessage)
                {
                    GUIStyle congratsStyle = new GUIStyle(GUI.skin.label);
                    congratsStyle.fontSize = 30;
                    congratsStyle.normal.textColor = Color.green; 
                    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 40, 200, 100), "Parabéns!", congratsStyle);
                }

                if (showRestartButton && GUI.Button(new Rect(Screen.width / 2 - 60, 250, 120, 53), "RESTART"))
                {
                    RestartGame();
                }

                if (showMainMenuButton)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 100, 350, 200, 53), "MENU PRINCIPAL"))
                    {
                        ReturnToMainMenu();
                    }
                }
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
                    theBall.RestartGame(); // Chama o método para reiniciar a bola
                }
            }
            else
            {
                PlayerScore1 += 100;
                audioSource2.Play();
                objetosDestruidos++; // Incrementa a contagem de objetos destruídos.
            }
        }
    }

    public void ResetBall()
    {
        theBall.RestartGame(); // Chama o método para reiniciar a bola
    }

    public void RestartGame()
    {
        PlayerScore1 = 0;
        PlayerLives = 5;
        gameOver = false;
        levelComplete = false;
        showRestartButton = false;
        showCongratsMessage = false; // Reseta a variável da mensagem de parabéns
        theBall.RestartGame();
        foreach (var obj in objetosDestrutiveis)
        {
            obj.ResetObject();
        }
        objetosDestruidos = 0; // Reseta a contagem de objetos destruídos ao reiniciar o jogo.
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("fase2");
    }
    public void IncrementObjetosDestruidos()
    {
        objetosDestruidos++;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("menu"); 
    }
}
