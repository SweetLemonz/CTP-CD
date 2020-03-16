using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private GameObject npcTrigger;
    private bool interact;
    private bool hasInteracted = false;
    public GameObject interactText;

    public string[] dialogue;
    public string name;

    void Update()
    {
        if (interact && hasInteracted == false)
        {
            interactText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Player Interaction Happened");
                hasInteracted = true;
                Interact();
            }


        }
        else
        {
            interactText.SetActive(false);
        }
    }

    public void Interact()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = true;
            npcTrigger = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = false;
            npcTrigger = null;
            hasInteracted = false;
        }
    }
}