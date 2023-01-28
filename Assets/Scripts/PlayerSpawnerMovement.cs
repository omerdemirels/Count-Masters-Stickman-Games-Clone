using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSpawnerMovement : MonoBehaviour
{
   
    [SerializeField] private GameObject player;
    [SerializeField] private float zSpeed = 4f;
    [SerializeField] private float xSpeed = 8f;
    private float touchX, newXValue;
    private float maxXPos = 4.80f, minxXPos = -4.80f;
    public List<GameObject> playersList = new List<GameObject>();
    private bool isPlayerMoving;
    

    [SerializeField] private LayerMask wall;
    [SerializeField] public bool hold = false;
    [SerializeField] private TextMeshProUGUI playerCountText;
    
    void Start()
    {

        isPlayerMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving==false)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            touchX = Input.GetAxis("Mouse X");
        }
        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXPos, maxXPos);

        Vector3 playerPosition = new Vector3(newXValue, transform.position.y, transform.position.z+ zSpeed * Time.deltaTime); 
        transform.position = playerPosition;

        CheckXBoundry();
        playerCountText.text = playersList.Count.ToString();
    }
    private void CheckXBoundry()
    {
        float minX = transform.position.x;
        float maxX = transform.position.x;

        for (int i = 0; i <playersList.Count; i++)
        {
            if (playersList[i].transform.position.x < minX)
            {
                minX = playersList[i].transform.position.x;
            }
            if (playersList[i].transform.position.x > maxX)
            {
                maxX = playersList[i].transform.position.x;
            }
        }

        Vector3 LeftControl = new Vector3(minX, transform.position.y, transform.position.z);
        Vector3 RightControl = new Vector3(maxX, transform.position.y, transform.position.z);

        if (Physics.Raycast(LeftControl, Vector3.left, 0.5f, wall))
        {
            minxXPos = transform.position.x;
        }
        else
        {
            minxXPos = -4.75f;
        }

        if (Physics.Raycast(RightControl, Vector3.right, 0.5f, wall))
        {
            maxXPos = transform.position.x;
        }
        else
        {
            maxXPos = 4.75f;
        }
    }

    public void SpawnPlayer(string gateSign, int gateValue ) 
    {
      
        if (gateSign == "addition") 
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayer = Instantiate(player, GetPlayerPos(), Quaternion.identity, transform);
                playersList.Add(newPlayer);
            }
        }
        else if (gateSign == "multiply")
        {
            int newPlayerCount = (playersList.Count * gateValue) - playersList.Count;
            for (int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayer = Instantiate(player, GetPlayerPos(), Quaternion.identity, transform);
                playersList.Add(newPlayer);
            }
        }


    }
    public Vector3 GetPlayerPos()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        return newPos;

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
           
            isPlayerMoving = false;
           Time.timeScale = 0;
        }
    }
    public void PlayerKill(GameObject player)
    {
        playersList.Remove(player);
        Destroy(player);
        CheckPlayersCount();

    }
    private void CheckPlayersCount()
    {
        if (playersList.Count <=0)
        {
            StopPlayer();
        }
    }
    public void EnemyDetected(GameObject target)
    {
        isPlayerMoving = false;
        TendEnemy(target);
        
    }
    
    private void TendEnemy(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion tendRotation = Quaternion.LookRotation(direction);
        tendRotation.x = 0;
        tendRotation.z = 0;
        transform.rotation = tendRotation;

    }
    private void LookForward()
    {
        transform.rotation = Quaternion.identity;
    }
    public void AllEnemyDead()
    {
        LookForward();
        MovePlayer();
    }
    public void MovePlayer()
    {
        isPlayerMoving = true;
        
    }
    public void StopPlayer()
    {
        isPlayerMoving = false;
       

    }

    /*public void StartIdlePosAnim()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            Player player = playersList[i].GetComponent<Player>();
           

        }
    }*/
}
