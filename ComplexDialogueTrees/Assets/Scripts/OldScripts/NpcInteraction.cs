using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private GameObject npcTrigger;
    private bool interact;
    private bool hasInteracted = false;
    public GameObject interactText;
    public NpcDialogue npcDialog;

    void Start()
    {
   
    }
    void Update()
    {
        if (interact && hasInteracted == false)
        {
            interactText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Player Interaction Happened");
                hasInteracted = true;
                npcDialog.npcInteracted = true;
            }


        }
        else
        {
            interactText.SetActive(false);
        }
    }

    public void Interact()
    {
        npcDialog.npcInteracted = true;
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