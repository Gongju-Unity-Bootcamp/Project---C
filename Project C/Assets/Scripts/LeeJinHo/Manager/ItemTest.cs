using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    public ItemID Id { get; private set; }
    public ItemData itemData;

    public string Name { get; private set; }
    public int ItemType { get; private set; }
    public float AttakAdd { get; private set; }
    public float AttakMulti { get; private set; }
    public float AttakSpeedAdd { get; private set; }
    public float AttakSpeedMulti { get; private set; }
    public float Speed { get; private set; }
    public float Range { get; private set; }
    public int Cost { get; private set; }
    public string Sprite { get; private set; }
    public string AcquireSound { get; private set; }
    public string UseSound { get; private set; }

    private Rigidbody2D rb;
    private CircleCollider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private AudioSource audio;

    public void Init(ItemID id)
    {
        Id = id;
        itemData = Manager.Data.Item[Id];

        rb = gameObject.AddComponent<Rigidbody2D>();
        collider2D = gameObject.AddComponent<CircleCollider2D>();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        audio = gameObject.AddComponent<AudioSource>();

        rb.mass = 3;
        rb.drag = 999999;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        StatSetting();
    }

    void StatSetting()
    {
        this.Name = itemData.Name;
        this.ItemType = itemData.ItemType;
        this.AttakAdd = itemData.AttakAdd;
        this.AttakMulti = itemData.AttakMulti;
        this.AttakSpeedAdd = itemData.AttakSpeedAdd;
        this.AttakSpeedMulti = itemData.AttakSpeedMulti;
        this.Speed = itemData.Speed;
        this.Range = itemData.Range;
        this.Cost = itemData.Cost;
        this.Sprite = itemData.Sprite;
        this.AcquireSound = itemData.AcquireSound;
        this.UseSound = itemData.UseSound;

    }
}
