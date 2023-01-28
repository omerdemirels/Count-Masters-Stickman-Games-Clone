using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public EnemySpawnerController enemySpawner;
    private bool isEnemyAlive;
    void Start()
    {
        isEnemyAlive = true;
    }
    void FixedUpdate()
    {
        if (enemySpawner.isEnemyAttacking == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.fixedDeltaTime * 1.5f);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isEnemyAlive ==true)
        {
            isEnemyAlive = false;
            enemySpawner.EnemyAttackPlayer(collision.gameObject, this.gameObject);
        }
    }
}
