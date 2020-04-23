using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    public string itemName = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

}
