using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode
{
    public string Text;
    public string NpcName;
    public int NodeID = -1;
    public List<DialogueOption> Options;
    public List<DialogueHiddenOption> HiddenOptions;
}
