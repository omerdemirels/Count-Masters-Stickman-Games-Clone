using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{

    public GameObject enemyObject;
    public int enemyCount = 3;
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject player;
    public PlayerSpawnerMovement playerSpawner;
    public bool isEnemyAttacking;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawner = player.GetComponent<PlayerSpawnerMovement>();
        SpawnEnemy();
    }

 
    void Update()
    {
        
    }
    public void SpawnEnemy()
    {
        for (int i = 0; i <enemyCount;i++)
        {
            Quaternion enemyRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            GameObject enemy = Instantiate(enemyObject, GetEnemyPos(), enemyRotation, transform);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.player = player;
            enemyController.enemySpawner = this;
            enemyList.Add(enemy);
        }

    }
    public Vector3 GetEnemyPos()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            playerSpawner.EnemyDetected(gameObject);
            TendPlayer(other.gameObject);
            isEnemyAttacking = true;
        }
    }
    private void TendPlayer(GameObject target)
    {
        Vector3 direction = transform.position - target.transform.position;
        Quaternion directRotation = Quaternion.LookRotation(direction);
        directRotation.x = 0;
        directRotation.z = 0;
        transform.rotation = directRotation;
    }

    public void EnemyAttackPlayer(GameObject player, GameObject enemy)
    {
        enemyList.Remove(enemy);
        CheckEnemyCount();
        playerSpawner.PlayerKill(player);
        Destroy(enemy);
    }
    private void CheckEnemyCount()
    {
        if (enemyList.Count<=0)
        {
            playerSpawner.AllEnemyDead();
        }
    }
}
