using UnityEngine;

public class BaseItemObject : ScriptableObject
{
    public int Id;
    public bool stackable;
    [TextArea(15, 20)] public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    public Item(BaseItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}