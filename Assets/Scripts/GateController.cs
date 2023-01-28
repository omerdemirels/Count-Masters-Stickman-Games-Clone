using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



//public enum GateSign { additionSign,multiplySign }

public class GateController : MonoBehaviour
{
    private GameObject playerSpawner;
    private PlayerSpawnerMovement playerSpawnerMovement;
    private bool hasGateUsed;
    private ParentGatesController parentGatesController;
    public int gateValue;
    public TextMeshProUGUI gateText;
    //public GateSign gateSign;
    private string gateSign;
    string[] signs = new string[] { "multiply", "addition" };

    // Start is called before the first frame update
    void Start()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerMovement = playerSpawner.GetComponent<PlayerSpawnerMovement>();
        parentGatesController = transform.parent.gameObject.GetComponent<ParentGatesController>();

        AddGateSignAndValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hasGateUsed == false)
        {
            hasGateUsed = true;
            playerSpawnerMovement.SpawnPlayer(gateSign,gateValue);
            parentGatesController.CloseGates();
            Destroy(gameObject);
        }
    }
    private void AddGateSignAndValue()
    {
        string randomSign = signs[Random.Range(0, signs.Length)];

        gateSign = randomSign;

        if (gateSign == "addition")
        {
            int[] randomNumbers = new int[] { 5, 10};
            gateValue = randomNumbers[Random.Range(0, randomNumbers.Length)];
            
            gateText.text = "+" + gateValue.ToString();
        }
        else if (gateSign == "multiply")
        {
            gateValue= Random.Range(2, 4);
           
            gateText.text = "X" + gateValue.ToString();
        }
    }
}
