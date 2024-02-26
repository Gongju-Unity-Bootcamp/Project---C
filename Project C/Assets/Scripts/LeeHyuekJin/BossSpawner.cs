using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    private bool isCheck = false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isCheck)
        {
            //중간에 시간을 멈추고 컷신 적용
            Instantiate(boss,transform.position, Quaternion.identity);
            isCheck = true;
        }
    }

}
