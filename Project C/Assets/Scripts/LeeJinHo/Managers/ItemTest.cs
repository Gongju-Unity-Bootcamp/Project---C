using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemTest : MonoBehaviour
{
    public ItemID Id { get; private set; }
    public ItemData itemData;

    public string Name { get; private set; }
    public ItemType itemType { get; private set; }
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

    private void Aeake()
    {
        Debug.Log("������ �����ũ");
        //Init(ItemID.NormalBox, transform.position);
    }

    const int m_isBoxType = 4000;
    public void Init(ItemID id, Vector3 po)
    {
        if ((int)id > m_isBoxType)
        {
            return;
        }
        rb = gameObject.AddComponent<Rigidbody2D>();
        collider2D = gameObject.AddComponent<CircleCollider2D>();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        audio = gameObject.AddComponent<AudioSource>();

        rb.mass = 3;
        rb.drag = 999999;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.tag = "Item";

        Id = id;
        itemData = Managers.Data.Item[Id];

        this.Name = itemData.Name;
        this.itemType = (ItemType)itemData.ItemType;
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

        transform.position = po - Vector3.forward;
        transform.localScale = new Vector2(8, 8);
        collider2D.radius = 0.05f;
        spriteRenderer.sprite = Managers.Resource.LoadSprite(Sprite);
        audio.clip = Managers.Resource.LoadAudioClips(AcquireSound);
        if (this.itemType == ItemType.Active)
        {
            //UseSound�� �������ִ� ��
        }
        Debug.Log($"{gameObject},{Id.ToString()}");

}
    
    //�ı��� ������ �޼ҵ�
    private void OnDisable()
    {

        audio.Play();
        Managers.Spawn.m_Items.Push(this.gameObject);
        gameObject.SetActive(false);
    }


    //�÷��̾�� �������� ������� �ı��Ǵ°Ŵ� �÷��̾�� ����? �ƴϸ� �����ۿ��� ����?
    //�ı� ó���� ���ҽ��Ŵ���?, ������?, �÷��̾�?


}
