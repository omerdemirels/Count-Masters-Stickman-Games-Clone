using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerSpawnerMovement playerSpawnerMovement;
    private GameObject playerSpawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerMovement = playerSpawner.GetComponent<PlayerSpawnerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            playerSpawnerMovement.PlayerKill(other.gameObject);
        }
    }
}
