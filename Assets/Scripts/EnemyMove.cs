using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Header("AttackType")]
    public bool melee;
    public bool ranged;
    public bool bigEnemy;

    [Header("Inscribed")]
    public int health;
    public float moveSpeed;
    public Transform moveTarget;
    public float stopRange;
    public int damage;
    public Transform firePoint;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate;
    public int spawnCost;
    public int goobletDropCount;

    public bool hasDamaged;

    [Header("Gooblet Drop")]
    public GameObject gooblet;
    public float burstRadius = 1f;

    private GameObject playerGo;
    private Enemy_Projectile enemyProjectile;
    private float fireTimer;
    private Animator enemyAnimator;
    private SphereCollider rightFirst;

    public void Awake()
    {

        playerGo = GameObject.FindWithTag("Player");
        moveTarget = playerGo.transform;
        
        if (melee)
        {
            int bonusHealth = 0;
            int bonusDamage = 0;
            int bonusGoobletDrop = 0;

            health = 1;
            damage = 1;
            goobletDropCount = 1;
            if (GameManager.Instance != null)
            {
                bonusHealth = GameManager.Instance.extraEnemy1Health;
                bonusDamage = GameManager.Instance.extraEnemy1Damage;
                bonusGoobletDrop = GameManager.Instance.extraEnemy1GoobletDrop;
            }

            health += bonusHealth;
            damage += bonusDamage;
            goobletDropCount += bonusGoobletDrop;
        }

        if (ranged) 
        {
            
            enemyProjectile = projectile.GetComponent<Enemy_Projectile>();
            int projectileDamage = enemyProjectile.damage;
            projectileDamage = 1;
            int bonusHealth = 0;
            int bonusDamage = 0;
            int bonusGoobletDrop = 0;

            health = 2;
            goobletDropCount = 3;

            if (GameManager.Instance != null)
            {
                bonusHealth = GameManager.Instance.extraEnemy2Health;
                bonusDamage = GameManager.Instance.extraEnemy2Damage;
                bonusGoobletDrop = GameManager.Instance.extraEnemy2GoobletDrop;
            }

            health += bonusHealth;
            projectileDamage += bonusDamage;
            goobletDropCount += bonusGoobletDrop;

        }

        if (bigEnemy)
        {
            int bonusHealth = 0;
            int bonusDamage = 0;
            int bonusGoobletDrop = 0;

            health = 3;
            damage = 2;
            goobletDropCount = 5;
            if (GameManager.Instance != null)
            {
                bonusHealth = GameManager.Instance.extraEnemy3Health;
                bonusDamage = GameManager.Instance.extraEnemy3Damage;
                bonusGoobletDrop = GameManager.Instance.extraEnemy3GoobletDrop;
            }

            health += bonusHealth;
            damage += bonusDamage;
            goobletDropCount += bonusGoobletDrop;
        }
    }

    public void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rightFirst = GetComponentInChildren<SphereCollider>();
        fireTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bigEnemy)
        {
            fireTimer -= Time.deltaTime;
            Vector2 playerXZ = new Vector2(moveTarget.position.x, moveTarget.position.z);
            float playerY = moveTarget.position.y;
            float enemyY = transform.position.y;
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
        else
        {
            fireTimer -= Time.deltaTime;
            Vector2 playerXZ = new Vector2(moveTarget.position.x, moveTarget.position.z);
            Vector3 enemyXZ = new Vector2(transform.position.x, transform.position.z);
            
            float distanceToPlayer = Vector3.Distance(enemyXZ, playerXZ);
            if (distanceToPlayer > stopRange)
            {
                FacePlayer();
                Move();
                RangedAttack();
                
            }
            else
            {
                MeleeAttack();
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
        Vector3 targetPos = moveTarget.position;
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
        hasDamaged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null) 
        {
            if (hasDamaged) return;

            Debug.Log("Hit Player!");
            player.TakeDamage(damage);
            hasDamaged = true;

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

    public void EnemyDie()
    {
        for (int i = goobletDropCount; i > 0; i--) 
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * burstRadius;
            spawnPos.y = transform.position.y + 0.3f;
            Instantiate(gooblet, spawnPos, gooblet.transform.rotation);
            Debug.Log("GoobletSpawned");
        }
    }
}
