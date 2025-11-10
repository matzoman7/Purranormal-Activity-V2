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
                
                
                enemy.health--;
            }
            
            if(enemy.health == 0)
            {
                Debug.Log("EnemyDieCalled");
                enemy.EnemyDie();
                Destroy(other.gameObject);
                
            }
        }
    }
}
