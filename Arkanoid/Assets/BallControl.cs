using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    private GameManager gameManager; // Adicione uma refer�ncia ao GameManager
    private GameM gameManager2;
    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, 10)); // For�a aleat�ria na dire��o diagonal superior direita
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, 10)); // For�a aleat�ria na dire��o diagonal superior esquerda
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
        audioSource = GetComponent<AudioSource>();

        // Encontre o GameManager na cena
        gameManager = FindObjectOfType<GameManager>();
        gameManager2 = FindObjectOfType<GameM>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
        }
        audioSource.Play();

        if (coll.collider.CompareTag("Bottomwall"))
        {
            gameManager.Score("Bottomwall"); // Notifique o GameManager para perder uma vida
            RestartGame(); // Reinicie a bola
        }
        if(coll.collider.CompareTag("Bottomwall"))
        {
            gameManager2.Score("Bottomwall"); // Notifique o GameManager para perder uma vida
            RestartGame(); // Reinicie a bola
        }
    }

    public void RestartGame()
    {
        rb2d.velocity = Vector2.zero;
        // Define a posi��o X como zero (centralizada) e a posi��o Y como -2.
        transform.position = new Vector2(0, -2);
        Invoke("GoBall", 1);
    }
}
