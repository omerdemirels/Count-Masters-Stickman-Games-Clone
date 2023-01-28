using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    public static GameManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance!=null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaytoTap()
    {
        panel.gameObject.SetActive(false);
        GameObject playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        PlayerSpawnerMovement playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerMovement>();
        playerSpawnerScript.MovePlayer();

    }
}
