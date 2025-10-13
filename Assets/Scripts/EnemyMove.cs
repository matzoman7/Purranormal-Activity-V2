using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("AttackType")]
    public bool melee;
    public bool ranged;
    
    [Header("Inscribed")]
    public float moveSpeed;
    public Transform moveTarget;
    public float stopRange;
    public int damage;
    public Transform firePoint;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate;

    private float fireTimer;
    private Animator enemyAnimator;
    private SphereCollider rightFirst;
    public void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rightFirst = GetComponentInChildren<SphereCollider>();
        fireTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        Vector2 playerXZ = new Vector2(moveTarget.position.x, moveTarget.position.z);
        Vector2 enemyXZ = new Vector2(transform.position.x, transform.position.z);
        float distanceToPlayer = Vector3.Distance(enemyXZ, playerXZ);
        if (distanceToPlayer > stopRange)
        {
            FacePlayer();
            Move();
        }
        else
        {
            if (melee)
            {
                MeleeAttack();
            }
            if (ranged) 
            { 
                RangedAttack();
            }

        }   
    }

    public void FacePlayer()
    {
        Vector3 directionOfPlayer = moveTarget.position - transform.position;
        directionOfPlayer.y = 0;
        if (directionOfPlayer != Vector3.zero) 
        { 
            Quaternion targetRotation = Quaternion.LookRotation(directionOfPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime *5f);
        }
    }

    public void Move()
    {
        Vector3 targetPos = new Vector3(moveTarget.position.x, transform.position.y, moveTarget.position.z); //Calculate new position as to not change the enemys y pos
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

    }

    public void MeleeAttack()
    {
        if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("TestEnemy"))
        {
            enemyAnimator.SetTrigger("Attack");
        }
        
    }

    public void EnableMeleeAttack()
    {
        rightFirst.enabled = true;
    }

    public void DisableMeleeAttack()
    {
        rightFirst.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null) 
        {
            //Debug.Log("Hit Player!");
            player.TakeDamage(damage);
        }
    }

    public void RangedAttack()
    {
        
        if(fireTimer <= 0)
        {
            Debug.Log("Fire bullet");
            GameObject tempProjectile = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
            Rigidbody projectileRig = tempProjectile.GetComponent<Rigidbody>();
            projectileRig.AddForce(projectileRig.transform.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(tempProjectile, 5f);

            fireTimer = 1f / fireRate;
        }
    }
}
