using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;

public class NpcDialogue : MonoBehaviour
{

    private Dialogue dialog;
    private GameObject dialogueWindow;
    private GameObject npcText;
    private GameObject option1;
    private GameObject option2;
    private GameObject option3;
    private GameObject exit;

    private int optionSelected = -2;
    public string DataFilePath;
    public GameObject DialogueWindowPrefab;

    void Start()
    {
        dialog = loadDialogue("Assets/Resources/newdiag.xml");
        var canvas = GameObject.Find("Canvas");
   
        dialogueWindow = Instantiate<GameObject>(DialogueWindowPrefab);
        dialogueWindow.transform.SetParent(canvas.transform, false);

        RectTransform dialogueWindowTransform = (RectTransform)dialogueWindow.transform;
        dialogueWindowTransform.localPosition = new Vector3(0, 0, 0);

        npcText = GameObject.Find("NPCText");
        option1 = GameObject.Find("ButtonOption1");
        option2 = GameObject.Find("ButtonOption2");
        option3 = GameObject.Find("ButtonOption3");
        exit = GameObject.Find("ButtonExit");

        exit.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(-1); });

        dialogueWindow.SetActive(false);

        RunDialogue();
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
    }

    private void displayNode(DialogueNode node)
    {
        npcText.GetComponent<Text>().text = node.Text;

        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);

        for (int i = 0; i < node.Options.Count && i < 3; i++)
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
            }
        }
    }

    private void setOptionButton(GameObject button, DialogueOption opt)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.Text;
        button.GetComponent<Button>().onClick.AddListener(delegate { SetOptionSelected(opt.NewNodeID); });
    }
}

