using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

using DG.Tweening;
using UnityEngine.UIElements;


public class Item : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Player") && !gameObject.CompareTag("Box")) 
        {
            Debug.Log("아이템획득");
            Managers.Sound.EffectSoundChange(AcquireSound);
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            _collider2D = GetComponent<Collider2D>();
            transform.Rotate(90, 0, 0);
            _collider2D.enabled = false;
            if (this.itemType == ItemType.Passive)
            {
                playerStats.UpdateStats(AttakAdd, AttakMulti, AttakSpeedAdd, AttakSpeedMulti, Speed, Range);

            }
            else if (this.itemType == ItemType.Active)
            {
                //클래스명을 지정함.
                m_ActieisItemName = string.Concat("Item_", Name);
                //매게변수로 전달하는 transform에게 액티브기능의 클래스를 부착하므로, 부착할 오브젝트로 지정해주면됨.
                //Managers.Item.AddComponent(m_ActieisItemName, transform);
                Managers.UI.GetPassive();
            }
            else if (this.itemType == ItemType.Consumer)
            {
                GetConsumerItem((int)Id, playerStats);
            }
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
    //private Vector3[] _transformsForShaking;
    //public float ShakeDuration = 0.6f;

    //private void OnEnable()
    //{
    //    if (gameObject.name != "NormalBox" && gameObject.name != "GoldenBox")
    //    {
    //        _transformsForShaking = new Vector3[2];
    //        _transformsForShaking[0] = transform.position;
    //        _transformsForShaking[1] = transform.position + new Vector3(0, 0.2f, 0);

    //        DoShake();
    //    }
    //}

    //private void DoShake()
    //{
    //    var sequence = DOTween.Sequence();
    //    sequence.Append(transform.DOPath(GetWaypointPositions(), ShakeDuration, PathType.CatmullRom))
    //            .SetEase(Ease.Linear)
    //            .SetLoops(-1, LoopType.Yoyo);
    //}

    //private Vector3[] GetWaypointPositions()
    //{
    //    Vector3[] positions = new Vector3[_transformsForShaking.Length * 2];
    //    for (int i = 0; i < _transformsForShaking.Length; i++)
    //    {
    //        positions[i * 2] = _transformsForShaking[i] + new Vector3(0, 0.1f, 0);
    //        positions[i * 2 + 1] = _transformsForShaking[i] - new Vector3(0, 0.1f, 0);
    //    }
    //    return positions;
    //}
    #endregion
}
