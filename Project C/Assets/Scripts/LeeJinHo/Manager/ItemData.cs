public enum ItemID
{
    None = 1000,
    Passive = 1000,
    Active = 2000,
    Consume = 3000,
    NormalBox = 4000,
    GoldenBox = 5000
}

public enum ItemType
{
    None = 0,
    Passive,
    Active,
    Consumer,
    NormalBox,
    GoldenBox
}
public class ItemData
{
    public ItemID Id { get; set; }
    public string Name { get; set; }
    public int ItemType { get; set; }
    public float AttakAdd { get; set; }
    public float AttakMulti { get; set; }
    public float AttakSpeedAdd { get; set; }
    public float AttakSpeedMulti { get; set; }
    public float Speed { get; set; }
    public float Range { get; set; }
    public int Cost { get; set; }
    public string Sprite { get; set; }
    public string AcquireSound { get; set; }
    public string UseSound { get; set; }

}
