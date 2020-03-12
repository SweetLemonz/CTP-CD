using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogTree
{
    public string text;
    public List<string> dialogText;
    public List<DialogTree> nodes;
    public void parseXML(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        XmlNode node = xmlDoc.SelectSingleNode("//dialoguetree/dialoguebranch");
        text = node.InnerText;
        XmlNodeList myNodeList = xmlDoc.SelectNodes("dialoguebranch/dialoguebranch");
        foreach (XmlNode node1 in myNodeList)
        {
            if (node1.InnerXml.Length > 0)
            {
                DialogTree dialogtreenode = new DialogTree();
                dialogtreenode.parseXML(node1.InnerXml);
                nodes.Add(dialogtreenode);
            }
        }
    }
}

public class XMLreaderOld : MonoBehaviour
{
    public TextAsset xmlRawFile;
    public Text uiText;
    public Text option1;
    public Text option2;
    public GameObject dialoguePanelv2;
    Button yesButton;
    Button noButton;
    int dialogueIndex = 1;
    string xmlPathPattern = "//dialoguetree/dialoguebranch";
    DialogTree dialogtree;

    void Awake()
    {
        string data = xmlRawFile.text;
        //parseXml(data);
        dialogtree = new DialogTree();
        dialogtree.parseXML(data);

        yesButton = dialoguePanelv2.transform.Find("Option1").GetComponent<Button>();
        yesButton.onClick.AddListener(delegate { ContinueYesDialogue(); });

        noButton = dialoguePanelv2.transform.Find("Option2").GetComponent<Button>();
        noButton.onClick.AddListener(delegate { ContinueNoDialogue(); });

        string textUi = "";
        textUi += dialogtree.text;
        uiText.text = textUi;
    }

    void parseXmlFile(string xmlData)
    {
       
        xmlPathPattern = "//dialoguetree/dialoguebranch";
        string dIndex = dialogueIndex.ToString();
        string textUi = "";
        string opt1Ui = "";
        string opt2Ui = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        xmlPathPattern += dIndex;
        Debug.Log(xmlPathPattern);

        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
            XmlNode text = node.FirstChild;
            XmlNode opt1 = text.NextSibling;
            XmlNode opt2 = opt1.NextSibling;

            textUi += text.InnerXml;
            uiText.text = textUi;

            opt1Ui += opt1.InnerXml;
            option1.text = opt1Ui;

            opt2Ui += opt2.InnerXml;
            option2.text = opt2Ui;

            Debug.Log(textUi);
        }
    }

    public void ContinueYesDialogue()
    {
        string data = xmlRawFile.text;

        if (dialogueIndex == 1)
        {
            dialogueIndex = 2;
            parseXmlFile(data);
            return;
        }

        if (dialogueIndex == 2)
        {
            dialogueIndex = 4;
            parseXmlFile(data);
            return;
        }

        if (dialogueIndex == 3)
        {
            dialogueIndex = 6;
            parseXmlFile(data);
            return;
        }

    }

    public void ContinueNoDialogue()
    {
        string data = xmlRawFile.text;

        if (dialogueIndex == 1)
        {
            dialogueIndex = 3;
            parseXmlFile(data);
            return;
        }

        if (dialogueIndex == 2)
        {
            dialogueIndex = 5;
            parseXmlFile(data);
            return;
        }

        if (dialogueIndex == 3)
        {
            dialogueIndex = 7;
            parseXmlFile(data);
            return;
        }

    }
}

