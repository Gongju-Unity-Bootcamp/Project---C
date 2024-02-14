using UnityEngine;

public class RandomMonsterMovement : MonoBehaviour
{
    public float moveSpeed;
    private float changeDirectionInterval = 2f;
    private float timeSinceLastDirectionChange = 0f;
    private Vector2 currentDirection;

    void Update()
    {

        timeSinceLastDirectionChange += Time.deltaTime;

        // ���� �ð��� ������ ���� ����
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
            timeSinceLastDirectionChange = 0f;
        }

        MoveRandomly();
    }

    void MoveRandomly()
    {
        transform.Translate(currentDirection.normalized * moveSpeed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
