using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerMovement : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField] private float zSpeed = 4f;
    [SerializeField] private float xSpeed = 8f;
    private float touchX, newXValue;
    private float maxXPos= 4.65f;
    public List<GameObject> playersList = new List<GameObject>();
    private bool isPlayerMoving;
    void Start()
    {
        isPlayerMoving = true;
        //SpawnPlayer();
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

        Vector3 playerPosition = new Vector3(newXValue, transform.position.y, transform.position.z + zSpeed * Time.deltaTime);
        transform.position = playerPosition;
    }
    public void SpawnPlayer(int gateValue, GateSign gateSign)
    {

        if (gateSign == GateSign.additionSign)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayer = Instantiate(player, GetPlayerPos(), Quaternion.identity, transform);
                playersList.Add(newPlayer);
            }
        }
        else if (gateSign == GateSign.multiplySign)
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
        if (other.tag == "FinishLine")
        {
            isPlayerMoving = false;
        }
    }
    public void PlayerKill(GameObject player)
    {
        playersList.Remove(player);
        Destroy(player);
    }
}
