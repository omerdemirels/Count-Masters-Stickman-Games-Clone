using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSpawnerMovement playerSpawnerMovement;
    private Transform center;
    [SerializeField] private float speed = 2;
    

    private void Start()
    {
        
    }
    private void Awake()
    {
        playerSpawnerMovement = GameObject.FindGameObjectWithTag("PlayerSpawner").GetComponent<PlayerSpawnerMovement>();
        center = GameObject.FindGameObjectWithTag("PlayerSpawner").transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!playerSpawnerMovement.hold)
        {
            transform.position = Vector3.MoveTowards(transform.position, center.position, Time.fixedDeltaTime * speed);
        }
    }

   
}
