using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("Inscribed")]
    public float moveSpeed;
    public Transform moveTarget;
    public float stopRange;

    
    
    

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, moveTarget.position);
        if (distanceToPlayer > stopRange)
        {
            Move();
        }   
    }

    

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
    }
}
