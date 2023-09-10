using UnityEngine;

public class obstaculo : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDestroyed = false;

    private void Start()
    {
        // Salvar a posição inicial do objeto.
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isDestroyed)
        {
            gameObject.SetActive(false);
            isDestroyed = true;

            // Aumentar o score
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.Score("ScoreObject");

            // Incrementar a contagem de objetos destruídos
            gameManager.IncrementObjetosDestruidos();
        }
    }

    // Método para reativar o objeto em sua posição inicial.
    public void ResetObject()
    {
        if (isDestroyed)
        {
            // Reativar o objeto e definir sua posição.
            gameObject.SetActive(true);
            transform.position = initialPosition;
            isDestroyed = false;
        }
    }
}
