
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IItem
{
    IItem m_Iitem { get; set; }

    ItemType Type { get; set; }
    void UsingItems();

}


public class Item : MonoBehaviour
{
    public float ShakeDuration = 0.25f;
    public ItemID Id { get; private set; }
    public ItemData itemData;
    private IItem Iitem;
    private string m_ActieisItemName;

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
    private Collider2D _collider2D;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer _spriteRenderer;
    private AudioClip clip;

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

        transform.position = po + Vector3.up;
        transform.localScale = new Vector2(2f, 2f);
        collider2D.enabled = true;
        collider2D.radius = 0.05f;
        spriteRenderer.sprite = Managers.Resource.LoadSprite(Sprite);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ((int)Id < 4000)) //박스에는 적용되면 안되는데 적용됨
        {
            Managers.Sound.EffectSoundChange(AcquireSound);
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
            _collider2D.enabled = false;
            if (this.itemType == ItemType.Passive)
            {
                playerStats.UpdateStats(AttakAdd, AttakMulti, AttakSpeedAdd, AttakSpeedMulti, Speed, Range);

            }
            else if (this.itemType == ItemType.Active)
            {
                m_ActieisItemName = string.Concat("Item_", Name);
                //매게변수로 전달하는 transform에게 액티브기능의 클래스를 부착하므로, 부착할 오브젝트로 지정해주면됨.
                //Managers.Item.AddComponent(m_ActieisItemName, transform);
                Managers.UI.GetPassive();
            }
            else if (this.itemType == ItemType.Consumer)
            {
                GetConsumerItem((int)Id, playerStats);
            }
            
            //Destroy(Managers.Item.DeathComponent(m_ActieisItemName));
        }

    }
    private void GetConsumerItem(int itemId, PlayerStats playerStats)
    {
        switch (itemId)
        {
            case 3001:
                playerStats.GetKey();
                break;
            case 3002:
                playerStats.GetBomb();
                break;

            case 3003:
            case 3004:
                int healAmount = (itemId == 3003) ? 1 : 2;
                if (playerStats.hp < 8)
                    playerStats.GetHp(healAmount);
                break;
        }
        Managers.UI.GetConsumer();
    }

    //게임오브젝트가 활성화 되면 그 위아래로 왔다 갔다 할 메소드(미완)
    #region
    //private void OnEnable()
    //{
    //    DoShake();
    //}

    //private void DoShake()
    //{
    //    var sequence = DOTween.Sequence();
    //    sequence.Append(transform.DOLocalMoveY(0.1f, ShakeDuration))
    //        .Append(transform.DOLocalMoveY(-0.1f, ShakeDuration))
    //        .SetLoops(-1);
    //}

    //private Transform[] _trasnformsForShaking;
    //private IEnumerator ShakeCo()
    //{
    //    float elapsedTime = 0;
    //    while (true)
    //    {

    //        // 1. 위로 간다.
    //        transform.position = Vector3.Lerp(_trasnformsForShaking[0].position,
    //            _trasnformsForShaking[1].position,
    //            elapsedTime / ShakeDuration);



    //        elapsedTime += Time.deltaTime;


    //    }
    //}
    #endregion
}
