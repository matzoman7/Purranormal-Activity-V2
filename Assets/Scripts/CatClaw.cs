using System.Collections;
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
                enemy.EnemyDie();
                Debug.Log("EnemyDieCalled");
            }
            
            Destroy(other.gameObject);
        }
    }
}
