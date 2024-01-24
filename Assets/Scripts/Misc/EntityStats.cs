using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [Header("HP")]
    public int hp;
    public int maxHp;
    
    [Header("Speed")]
    public float baseSpeed;

    [Header("Attack")]
    public int attackDamage;
    public float attackSpeed;

    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
