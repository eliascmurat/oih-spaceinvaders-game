using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private EntityStats _es;
    
    public GameObject bulletObject;
    
    [SerializeField] private float cooldownCounter;
    [SerializeField] private bool canAttack = true;

    public void Start()
    {
        _es = gameObject.GetComponent<EntityStats>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && canAttack)
        {
            Instantiate(bulletObject, transform.position, Quaternion.identity);
            canAttack = false;
            cooldownCounter = 0;
        }

        Cooldown();
    }

    private void Cooldown()
    {
        if (cooldownCounter > _es.attackSpeed && !canAttack)
        {
            canAttack = true;
        }
        else
        {
            cooldownCounter += Time.deltaTime;
        }
    }
}
