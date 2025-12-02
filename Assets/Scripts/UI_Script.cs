using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Sprite UISprite;
    public GameObject health2;
    public GameObject health2Text;
    public GameObject health3;
    public GameObject health3Text;
    public GameObject damage2;
    public GameObject damage2Text;
    public GameObject damage3;
    public GameObject damage3Text;
    public GameObject enemy2;
    public GameObject enemy2Text;
    public GameObject enemy3;
    public GameObject enemy3Text;

    public GameManager gameManager;

    public GameObject popUp;
    public GameObject alreadyBoughtText;
    public GameObject newPurchaseText;
    public GameObject notEnoughGoobsText;

    public void ClosePopUp()
    {
        alreadyBoughtText.SetActive(false);
        newPurchaseText.SetActive(false);
        notEnoughGoobsText.SetActive(false);
        popUp.SetActive(false);
    }

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


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

        if (GameManager.Instance.healthUpgradeLevel == 0 && gameManager.totalGooblets >= 3)
        {
            GameManager.Instance.UpgradeHealth();
            health2Text.SetActive(true);
            health2.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.healthUpgradeLevel > 0)
        {
            
            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 3)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }
    public void HealthUpgrade2()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.healthUpgradeLevel == 1 && gameManager.totalGooblets >= 6)
        {
            GameManager.Instance.UpgradeHealth();
            health3Text.SetActive(true);
            health3.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.healthUpgradeLevel > 1)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 6)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }

    public void HealthUpgrade3()//Later when we get all upgrades done. I want the player to purchases each level one before buying any level 2.
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.healthUpgradeLevel == 2 && gameManager.totalGooblets >= 9)
        {
            GameManager.Instance.UpgradeHealth();
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.healthUpgradeLevel < 2)//Cant but level 3 unless they have 1 and 2
        {
            Debug.Log("Buy Upgrade 2 first!");
        }
        else if (GameManager.Instance.healthUpgradeLevel > 2)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 9)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }

    public void DamageUpgrade1()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 0 && gameManager.totalGooblets >= 3)
        {
            GameManager.Instance.UpgradeDamage();
            damage2Text.SetActive(true);
            damage2.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.damageUpgradeLevel > 0)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 3)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }
    public void DamageUpgrade2()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 1 && gameManager.totalGooblets >= 6)
        {
            GameManager.Instance.UpgradeDamage();
            damage3Text.SetActive(true);
            damage3.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.damageUpgradeLevel < 1)
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else if (GameManager.Instance.damageUpgradeLevel > 1)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 6)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }
    public void DamageUpgrade3()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.damageUpgradeLevel == 2 && gameManager.totalGooblets >= 9)
        {
            GameManager.Instance.UpgradeDamage();
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.damageUpgradeLevel < 2)
        {
            Debug.Log("Buy Upgrade 2 first!");
        }
        else if (GameManager.Instance.damageUpgradeLevel > 2)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 9)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }
    public void EnemyUpgrade1()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 0 && gameManager.totalGooblets >= 5)
        {
            GameManager.Instance.UpgradeEnemy();
            enemy2Text.SetActive(true);
            enemy2.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.enemyUpgradeLevel > 0)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 5)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }

    }

    public void EnemyUpgrade2()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 1 && gameManager.totalGooblets >= 10)
        {
            GameManager.Instance.UpgradeEnemy();
            enemy3Text.SetActive(true);
            enemy3.GetComponent<Image>().sprite = UISprite;
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.enemyUpgradeLevel < 1)//Cant but level 2 unless they have 1.
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else if (GameManager.Instance.enemyUpgradeLevel > 1)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 10)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }

    public void EnemyUpgrade3()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.enemyUpgradeLevel == 2 && gameManager.totalGooblets >= 15)
        {
            GameManager.Instance.UpgradeEnemy();
            popUp.SetActive(true);
            newPurchaseText.SetActive(true);
        }
        else if (GameManager.Instance.enemyUpgradeLevel < 2)//Cant but level 2 unless they have 1.
        {
            Debug.Log("Buy Upgrade 1 first!");
        }
        else if (GameManager.Instance.enemyUpgradeLevel > 2)
        {

            Debug.Log("You’ve already bought this or a higher level!");
            alreadyBoughtText.SetActive(true);
            popUp.SetActive(true);
        }
        else if (gameManager.totalGooblets < 15)
        {
            popUp.SetActive(true);
            notEnoughGoobsText.SetActive(true);
        }
    }
}
