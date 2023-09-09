using UnityEngine;

public class obstaculo : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDestroyed = false;

    private void Start()
    {
        // Salve a posição inicial do objeto.
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isDestroyed)
        {
            // Desative o objeto quando atingido pela bola.
            gameObject.SetActive(false);
            isDestroyed = true;
        }
    }

    // Método para reativar o objeto em sua posição inicial.
    public void ResetObject()
    {
        if (isDestroyed)
        {
            // Reative o objeto e defina sua posição.
            gameObject.SetActive(true);
            transform.position = initialPosition;
            isDestroyed = false;
        }
    }
}
