using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletObject;

    public void Attack()
    {
        Instantiate(bulletObject, transform.position, Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle") && !other.CompareTag("Player")) return;
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
