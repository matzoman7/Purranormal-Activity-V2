using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    
    
    
    public void GoToUpgrades()
    {
        SceneManager.LoadScene("Upgrades_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game_Scene");//If you want to test upgrades in your scene change this to your scene name. But change back when you push up.
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }

    public void HealthUpgrade1()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.healthUpgradeLevel == 0)
        {
            GameManager.Instance.UpgradeHealth();
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }     
    }
    public void HealthUpgrade2()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.healthUpgradeLevel == 1)
        {
            GameManager.Instance.UpgradeHealth();
        }
        else if (GameManager.Instance.healthUpgradeLevel < 1)//Cant but level 2 unless they have 1.
        {
           Debug.Log("Buy Upgrade 1 first!");
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }         
    }

    public void HealthUpgrade3()//Later when we get all upgrades done. I want the player to purchases each level one before buying any level 2.
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.healthUpgradeLevel == 2)
        {
            GameManager.Instance.UpgradeHealth();
        }
        else if (GameManager.Instance.healthUpgradeLevel < 2)//Cant but level 3 unless they have 1 and 2
        {
            Debug.Log("Buy Upgrade 2 first!");
        }
        else
        {
            Debug.Log("You already have max health!");
        }            
    }

    public void DamageUpgrade1()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 0)
        {
            GameManager.Instance.UpgradeDamage();
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }
    }
    public void DamageUpgrade2()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 1)
        {
            GameManager.Instance.UpgradeDamage();
        }
        else if (GameManager.Instance.damageUpgradeLevel < 1)
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }
    }
    public void DamageUpgrade3()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 2)
        {
            GameManager.Instance.UpgradeDamage();
        }
        else if (GameManager.Instance.damageUpgradeLevel < 2)
        {
            Debug.Log("Buy Upgrade 2 first!");
        }
        else
        {
            Debug.Log("You already have max damage!");
        }
    }
    public void EnemyUpgrade1()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 0)
        {
            GameManager.Instance.UpgradeEnemy();
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }

    }

    public void EnemyUpgrade2()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 1)
        {
            GameManager.Instance.UpgradeEnemy();
        }
        else if (GameManager.Instance.enemyUpgradeLevel < 1)//Cant but level 2 unless they have 1.
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }
    }

    public void EnemyUpgrade3()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 2)
        {
            GameManager.Instance.UpgradeEnemy();
        }
        else if (GameManager.Instance.enemyUpgradeLevel < 2)//Cant but level 2 unless they have 1.
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else
        {
            Debug.Log("You’ve already bought this or a higher level!");
        }
    }
}
