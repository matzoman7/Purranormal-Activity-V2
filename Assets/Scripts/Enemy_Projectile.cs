using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    [Header("Inscribed")]
    public int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            //Debug.Log("Hit Player!");
            player.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        
    }
}
