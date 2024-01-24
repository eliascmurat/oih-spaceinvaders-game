using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    
    private const float StartSpawnX = -3.75f;
    private const float StartSpawnY = 4.25f;
    
    private const float SpacingX = 1.25f;
    private const float SpacingY = 1.25f;

    public GameObject enemiesContainer;
    private readonly Dictionary<int, Transform> _objectDictionary = new Dictionary<int, Transform>();
    
    public GameObject topLineEnemyGameObject;
    public GameObject midLineEnemyGameObject;
    public GameObject frontLineEnemyGameObject;
    
    public float timeToAttack;
    [SerializeField] private int rangeAttack;
    [SerializeField] private float cooldownCounter;
    [SerializeField] private bool canAttack = true;
    
    private void Awake() 
    { 
        if(Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    private void Start()
    {
        SpawnEnemies();
    }

    private void FixedUpdate()
    {
        IncreaseSpeed();
        
        if (canAttack)
        {
            EnemyAttack();

            canAttack = false;
            cooldownCounter = 0;
        }

        Cooldown();
    }

    private void SpawnEnemies()
    {
        for (var row = 0; row < 5; row++)
        {
            for (var col = 0; col < 7; col++)
            {
                var spawnPosition = new Vector3(StartSpawnX + col * SpacingX, StartSpawnY - row * SpacingY, 0);

                GameObject enemyPrefab;
                switch (row)
                {
                    case 0:
                        enemyPrefab = topLineEnemyGameObject;
                        break;
                    case 1:
                    case 2:
                        enemyPrefab = midLineEnemyGameObject;
                        break;
                    default:
                        enemyPrefab = frontLineEnemyGameObject;
                        break;
                }
                
                var enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                enemyInstance.transform.parent = enemiesContainer.transform;
            }
        }
        
        var index = 0;
        foreach (Transform child in enemiesContainer.transform)
        {
            SetEnemy(index, child);
            index++;
        }
    }

    private void EnemyAttack()
    {
        var enemyId = Random.Range(1, 36);
        var enemyReference = GetEnemy(enemyId);

        if (enemyReference.IsUnityNull()) return;
        
        var rollDice = Random.Range(1, 11);

        if (rollDice > rangeAttack)
            enemyReference.GetComponent<EnemyAttack>().Attack();
    }

    private void IncreaseSpeed()
    {
        var childCount = enemiesContainer.transform.childCount;
        if (childCount <= 0) return;

        var maxSpeed = true switch
        {
            _ when childCount < 7 => 0.3f,
            _ when childCount is >= 7 and < 14 => 0.6f,
            _ when childCount is >= 14 and < 21 => 0.9f,
            _ when childCount is >= 21 and < 28 => 1.2f,
            _ => 1.5f
        };
        
        var horizontal = true switch
        {
            _ when childCount is >= 1 and < 2 => 1.75f,
            _ when childCount is >= 2 and < 7 => 1.65f,
            _ when childCount is >= 7 and < 14 => 1.55f,
            _ when childCount is >= 14 and < 21 => 1.45f,
            _ when childCount is >= 21 and < 28 => 1.35f,
            _ => 1.25f
        };
        

        foreach (Transform child in enemiesContainer.transform)
        {
            child.GetComponent<EnemyMovement>().timeToMove = maxSpeed;
            child.GetComponent<EnemyMovement>().spacingX = horizontal;
        }
    }
    
    private void Cooldown()
    {
        if (cooldownCounter > timeToAttack && !canAttack)
        {
            canAttack = true;
        }
        else
        {
            cooldownCounter += Time.deltaTime;
        }
    }
    
    private void SetEnemy(int id, Transform enemyTransform)
    {
        _objectDictionary.Add(id, enemyTransform);
    }

    private Transform GetEnemy(int id)
    {
        _objectDictionary.TryGetValue(id, out var result);
        return result;
    }
}
