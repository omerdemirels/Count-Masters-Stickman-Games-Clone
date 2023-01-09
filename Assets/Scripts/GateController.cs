using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public enum GateSign { additionSign,multiplySign }

public class GateController : MonoBehaviour
{
    private GameObject playerSpawner;
    private PlayerSpawnerMovement playerSpawnerMovement;
    private bool hasGateUsed;
    private ParentGatesController parentGatesController;
    public int gateValue;
    public TextMeshProUGUI gateText;
    public GateSign gateSign;
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
        if (other.tag == "Player" && hasGateUsed ==false)
        {
            hasGateUsed = true;
            playerSpawnerMovement.SpawnPlayer(gateValue,gateSign);
            parentGatesController.CloseGates();
            Destroy(gameObject);
        }
    }
    private void AddGateSignAndValue()
    {
        if (gateSign == GateSign.additionSign)
        {
            gateText.text = "+" + gateValue.ToString();
        }
        else if (gateSign == GateSign.multiplySign)
        {
            gateText.text = "X" + gateValue.ToString();
        }
    }
}
