using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public enum ItemIDTest
{
    None = 1000,
    Passive = 1001,
    Active = 2001,
    Consume = 3001,
    NormalBox = 4001,
    GoldenBox = 5001
}

public enum ItemTypeTest
{
    None = 0,
    Passive,
    Active,
    Consumer,
    NormalBox,
    GoldenBox
}
public class ItemDataTest
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public ItemType itemType { get; set; }
    public float AttackAdd { get; set; }
    public float AttakMulti { get; set; }
    public float AttackSpeedAdd { get; set; }
    public float AttackSpeedMulti { get; set; }
    public float Speed { get; set; }
    public int Range { get; set; }
    public int Cost { get; set; }
    public string Sprite { get; set; }
    public string AcquireSound { get; set; }
    public string UseSound { get; set; }
}
