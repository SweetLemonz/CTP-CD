using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool interact;
    private bool hasInteracted = false;
    private GameObject pickupText;

    public Item item;
    public string itemName;

    void Start()
    {
        pickupText = GameObject.Find("PickUpText");
    }
    void Update()
    {
        if (interact && hasInteracted == false)
        {
            pickupText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Player Interaction Happened");
                hasInteracted = true;
                PickUp();
            }


        }
        else
        {
            pickupText.SetActive(false);
        }
    }

    public void PickUp()
    {
        pickupText.SetActive(false);
        Debug.Log("Picking Up Item");
        Inventory.instance.addItem(itemName);
        Destroy(gameObject);

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = false;
            hasInteracted = false;
        }
    }
}

