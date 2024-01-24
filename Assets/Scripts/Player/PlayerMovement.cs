using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private EntityStats _es;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxXPosition;
    [SerializeField] private float minXPosition;
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _es = gameObject.GetComponent<EntityStats>();
        
        moveSpeed = _es.baseSpeed;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        LimitPlayerPosition();
    }

    private void HandleMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        _rb.AddForce(new Vector2(horizontal * moveSpeed * Time.deltaTime, 0));
    }
    
    private void LimitPlayerPosition()
    {
        var clampedX = Mathf.Clamp(_rb.position.x, minXPosition, maxXPosition);
        _rb.position = new Vector2(clampedX, _rb.position.y);
    }
}
