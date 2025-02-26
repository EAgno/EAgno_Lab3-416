using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        // Starting rotation
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);
    }

    void Update()
    {
        // Coin still rotates
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Conditional to see if player makes contact with coin
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.CollectCoin(); // Increase score
            }

            Destroy(gameObject); // Remove the coin
        }
    }
}
