using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;

public class NpcDialogue : MonoBehaviour
{
    public Inventory invent;
    private Dialogue dialog;
    private GameObject dialogueWindow;
    private GameObject npcText;
    private GameObject option1;
    private GameObject option2;
    private GameObject option3;
    private GameObject option4;
    private GameObject end;
    public GameObject npcObject;
    public bool hasObject;

    private GameObject npcTrigger;
    private bool interact;
    private bool hasInteracted = false;
    private GameObject interactText;

    private int optionSelected = -2;
    public string DataFilePath;
    public bool npcInteracted = false;
    public GameObject DialogueWindowPrefab;

    void Start()
    {
        hasObject = true;
        dialog = loadDialogue("Assets/Resources/" + DataFilePath);
        var canvas = GameObject.Find("Canvas");
   
        dialogueWindow = Instantiate<GameObject>(DialogueWindowPrefab);
        dialogueWindow.transform.SetParent(canvas.transform, false);

        RectTransform dialogueWindowTransform = (RectTransform)dialogueWindow.transform;
        dialogueWindowTransform.localPosition = new Vector3(0, 0, 0);

        interactText = GameObject.Find("InteractText");
        npcText = GameObject.Find("NPCText");
        option1 = GameObject.Find("ButtonOption1");
        option2 = GameObject.Find("ButtonOption2");
        option3 = GameObject.Find("ButtonOption3");
        option4 = GameObject.Find("ButtonOption4");
        end = GameObject.Find("ButtonEnd");

        end.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(-1); });

        dialogueWindow.SetActive(false);

            RunDialogue();
        
    }

    void Update()
    {
        if (interact && hasInteracted == false)
        {
            interactText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                hasInteracted = true;
                npcInteracted = true;
                RunDialogue();
                GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            interactText.SetActive(false);
        }
    }


    public static Dialogue loadDialogue(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Dialogue));
        StreamReader reader = new StreamReader(path);

        Dialogue dialog = (Dialogue)serializer.Deserialize(reader);
        return dialog;
    }

    public void RunDialogue()
    {
        StartCoroutine(run());
    }

    public void SetOptionSelected(int x)
    {
        optionSelected = x;
    }

    public IEnumerator run()
    {
        if (npcInteracted == true)
        {
            dialogueWindow.SetActive(true);

            int nodeID = 0;

            while (nodeID != -1)
            {
                displayNode(dialog.Nodes[nodeID]);
                optionSelected = -2;
                while (optionSelected == -2)
                {
                    yield return new WaitForSeconds(0.25f);
                }
                nodeID = optionSelected;
            }
            dialogueWindow.SetActive(false);
            npcInteracted = false;
            GameObject.Find("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void displayNode(DialogueNode node)
    {
        npcText.GetComponent<Text>().text = node.Text;

        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
        option4.SetActive(false);

        for (int i = 0; i < node.Options.Count && i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    setOptionButton(option1, node.Options[i]);
                    break;
                case 1:
                    setOptionButton(option2, node.Options[i]);
                    break;
                case 2:
                    setOptionButton(option3, node.Options[i]);
                    break;
                case 3:
                    setOptionButton(option4, node.Options[i]);
                    break;
                    //if (hasObject == true)
                    //{
                        //setHiddenOptionButton(option4, node.HiddenOptions[1]);
                       // break;
                   // }
                   // else
                       // break;
                    
                    // if (hasObject == true) // (npcObject in inventory)
                    // {
                    //    setOptionButton(option4, node.Options[i]);
                    //     break;
                    //  }
                    // else
                    // break;
            }
        }
       //if (hasObject == true)
       //{
       //    setHiddenOptionButton(option4, node.HiddenOptions[0]);
       //    return;
       //}
    }

    private void setOptionButton(GameObject button, DialogueOption opt)
    {
        //if (opt.Condition == 0)
        //{
        //    button.SetActive(true);
        //    button.GetComponentInChildren<Text>().text = opt.Text;
        //    button.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(opt.NewNodeID); });
        //}
        //else
        //    return;
        switch(opt.Condition)
        {
            case 0:
                button.SetActive(true);
                button.GetComponentInChildren<Text>().text = opt.Text;
                button.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(opt.NewNodeID); });
                break;

            case 1:
                if (invent.items.Contains(opt.Item))
                {
                    button.SetActive(true);
                    button.GetComponentInChildren<Text>().text = opt.Text;
                    button.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(opt.NewNodeID); });
                    break;
                }
                else
                {
                    break;
                }

            default:
                break;
        }
    }

    private void setHiddenOptionButton(GameObject button, DialogueHiddenOption hidOpt)
      {
         button.SetActive(true);
         button.GetComponentInChildren<Text>().text = hidOpt.Text;
         button.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(hidOpt.NewNodeID); });
      }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = true;
           // npcTrigger = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            interact = false;
           // npcTrigger = null;
            hasInteracted = false;
        }
    }
}

