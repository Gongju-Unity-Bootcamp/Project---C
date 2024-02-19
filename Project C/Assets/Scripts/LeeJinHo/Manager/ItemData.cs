using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public enum ItemID
{
    None = 1000,
    Passive = 1001,
    Active = 2001,
    Consume = 3001
}

public enum ItemType
{

}
public class ItemData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public ItemType itemType { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed { get; set; }
    public float Speed { get; set; }
    public int Range { get; set; }
    public int Cost { get; set; }
    public string Sprite { get; set; }
    public string AcquireSound { get; set; }
    public string UseSound { get; set; }
}
