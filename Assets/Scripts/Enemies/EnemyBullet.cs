using UnityEngine;

public class EnemyBullet : BulletController
{
    private Rigidbody2D _rb;

    [SerializeField] private int damageAmount;
    [SerializeField] private int moveSpeed;

    private void Start()
    {
        Destroy(gameObject, 2f);
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        _rb.AddForce(new Vector2(0, -moveSpeed), ForceMode2D.Impulse);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EntityStats>().hp -= damageAmount;
            Destroy(gameObject);
        }
    }
}
