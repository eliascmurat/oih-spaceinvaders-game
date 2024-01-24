using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _minX;
    private float _maxX;

    private float _direction;
    public float spacingX;
    public float spacingY;
    
    public float timeToMove;
    [SerializeField] private float cooldownCounter;
    [SerializeField] private bool canMove;

    private void Start()
    {
        _minX = -4f;
        _maxX = 4f;

        spacingX = 1.25f;
        spacingY = 1.25f;

        _direction = spacingX;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        
            canMove = false;
            cooldownCounter = 0;
        }
        
        Cooldown();
    }

    private void Move()
    {
        EnemyManager.Instance.enemiesContainer.transform.position += 
            new Vector3(_direction * Time.deltaTime, 0, 0);
        
        if (
            EnemyManager.Instance.enemiesContainer.transform.position.x >= _maxX || 
            EnemyManager.Instance.enemiesContainer.transform.position.x <= _minX
        ) 
        {
            ToggleDirection();
            
            EnemyManager.Instance.enemiesContainer.transform.position += 
                new Vector3(_direction * Time.deltaTime, -spacingY * Time.deltaTime, 0);
        }
    }

    private void ToggleDirection()
    {
        if (_direction > 0)
        {
            _direction = -spacingX;
        }
        else
        {
            _direction = spacingX;
        }
    }
    
    private void Cooldown()
    {
        if (cooldownCounter > timeToMove && !canMove)
        {
            canMove = true;
        }
        else
        {
            cooldownCounter += Time.deltaTime;
        }
    }
}
