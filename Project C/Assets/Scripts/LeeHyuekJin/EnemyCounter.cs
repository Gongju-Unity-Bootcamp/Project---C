using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private int enemyCount;
    private GameObject[] enemies;
    private Collider2D[] doorColliders;
    private bool isdoor = true;


    private void OnDestroy()
    {
        if(isdoor)
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            doorColliders = new Collider2D[doors.Length];
            for (int i = 0; i < doors.Length; i++)
            {
                doorColliders[i] = doors[i].GetComponent<Collider2D>();
            }
            Debug.Log(doors.Length);
            isdoor = false;
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        if (enemyCount == 0)
        {
            Debug.Log("적이 없음");
            foreach (Collider2D doorCollider in doorColliders)
            {
                if (doorCollider != null)
                {
                    doorCollider.isTrigger = false;
                }
            }
        }
    }
}