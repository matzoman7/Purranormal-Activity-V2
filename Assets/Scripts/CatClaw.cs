using UnityEngine;

public class CatClaw : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                // Enemy takes 1 damage
                int totalDamage = 1 + GameManager.Instance.extraDamage;//the upgrades
                enemy.health -= totalDamage;
                Debug.Log($"Dealt {totalDamage} damage! Enemy health: {enemy.health}");


                // Check if enemy died
                if (enemy.health <= 0)
                {
                    Debug.Log("EnemyDieCalled");
                    enemy.EnemyDie();
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
