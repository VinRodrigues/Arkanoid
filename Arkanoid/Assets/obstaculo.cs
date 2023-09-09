using UnityEngine;

public class obstaculo : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDestroyed = false;

    private void Start()
    {
        // Salve a posi��o inicial do objeto.
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

    // M�todo para reativar o objeto em sua posi��o inicial.
    public void ResetObject()
    {
        if (isDestroyed)
        {
            // Reative o objeto e defina sua posi��o.
            gameObject.SetActive(true);
            transform.position = initialPosition;
            isDestroyed = false;
        }
    }
}
