using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class XMLreader : MonoBehaviour
{
    public TextAsset xmlRawFile;
    public Text uiText;
    public GameObject dialoguePanel;
    Button continueButton;
    string xmlPathPattern = "//dialoguetree/dialoguebranch1a";
    int dialogueIndex;

    void Awake()
    {
        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
    
    }
    void Update()
    {
        string data = xmlRawFile.text;
        parseXmlFile(data);
        dialogueState();
    }

    void parseXmlFile(string xmlData)
    {
        string totText = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach(XmlNode node in myNodeList)
        {
            XmlNode text = node.FirstChild;

            totText += text.InnerXml;
            uiText.text = totText;

            Debug.Log(totText);
        }
    }

    void dialogueState()
    {
        if (dialogueIndex == 1)
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch2a";
            dialogueIndex = 1;
        }

        if (dialogueIndex == 0 && Input.GetKeyDown(KeyCode.N))
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch2b";
            dialogueIndex = 2;
            StartCoroutine(diagloueWait());
        }

        if (dialogueIndex == 1 && Input.GetKeyDown(KeyCode.M))
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch3a";
            dialogueIndex = 3;
            StartCoroutine(diagloueWait());
        }

        if (dialogueIndex == 1 && Input.GetKeyDown(KeyCode.N))
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch3b";
            dialogueIndex = 4;
            StartCoroutine(diagloueWait());
        }

        if (dialogueIndex == 2 && Input.GetKeyDown(KeyCode.M))
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch3c";
            dialogueIndex = 5;
            StartCoroutine(diagloueWait());
        }

        if (dialogueIndex == 2 && Input.GetKeyDown(KeyCode.N))
        {
            xmlPathPattern = "//dialoguetree/dialoguebranch3d";
            dialogueIndex = 6;
            StartCoroutine(diagloueWait());
        }

        if (dialogueIndex == 3)
        {
            dialoguePanel.SetActive(false);
            StartCoroutine(diagloueWait());
        }
    }

    public void ContinueDialogue()
    {
        dialogueIndex = 1;
    }

    IEnumerator diagloueWait()
    {
        yield return new WaitForSeconds(5);
    }

}
