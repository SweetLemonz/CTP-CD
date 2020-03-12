using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class XMLreaderOld : MonoBehaviour
{
    public TextAsset xmlRawFile;
    public Text uiText;
    public Text option1;
    public Text option2;
    public GameObject dialoguePanelv2;
    Button continueButton;
    Button yesButton;
    Button noButton;
    int dialogueIndex = 1;
    string xmlPathPattern = "//dialoguetree/dialoguebranch";

    void Awake()
    {
        string data = xmlRawFile.text;
        parseXmlFile(data);

        yesButton = dialoguePanelv2.transform.Find("Option1").GetComponent<Button>();
        yesButton.onClick.AddListener(delegate { ContinueYesDialogue(); });

        noButton = dialoguePanelv2.transform.Find("Option2").GetComponent<Button>();
        noButton.onClick.AddListener(delegate { ContinueNoDialogue(); });
    }

    void parseXmlFile(string xmlData)
    {
        xmlPathPattern = "//dialoguetree/dialoguebranch";
        string dIndex = dialogueIndex.ToString();
        string textUi = "";
        string opt1 = "";
        string opt2 = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        xmlPathPattern += dIndex;
        Debug.Log(xmlPathPattern);

        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
            XmlNode text = node.FirstChild;

            textUi += text.InnerXml;
            uiText.text = textUi;

            opt1 += text.NextSibling;
            option1.text = opt1;

            opt2 += text.NextSibling;
            option2.text = opt2;

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

