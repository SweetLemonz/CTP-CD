using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenNPCs : MonoBehaviour
{
    #region Singleton

    public static SeenNPCs instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public List<string> SeenNPCsList = new List<string>();

    public void addName(string seenNPCName)
    {
        if (SeenNPCsList.Contains(seenNPCName) || seenNPCName == null)
        {
            return;
        }
        else
        {
            SeenNPCsList.Add(seenNPCName);
        }
    }
}
