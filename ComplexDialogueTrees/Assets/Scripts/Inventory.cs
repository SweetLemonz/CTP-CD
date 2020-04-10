using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<string> items = new List<string>();

    public void addItem (string item)
    {
        items.Add(item);
    }

    public void removeItem (string item)
    {
        items.Remove(item);
    }

}
