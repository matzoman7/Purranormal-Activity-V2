using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Data")]
    public int totalGooblets;
    private TextMeshProUGUI goobletText;

    [Header("Upgrades")]
    public int extraHealth = 0; // each upgrade adds to this
    public int healthUpgradeLevel = 0; // 0 = none, 1 = level1, etc.
    public int extraDamage = 0; // each upgrade adds to this
    public int damageUpgradeLevel = 0; // 0 = none, 1 = level1, etc.

    [Header("Enemy Upgrades")]
    public int extraEnemy1Health = 0;
    public int extraEnemy2Health = 0;
    public int extraEnemy3Health = 0;
    public int extraEnemy1Damage = 0;
    public int extraEnemy2Damage = 0;
    public int extraEnemy3Damage = 0;
    public int extraEnemy1GoobletDrop = 0;
    public int extraEnemy2GoobletDrop = 0;
    public int extraEnemy3GoobletDrop = 0;
    public int enemyUpgradeLevel = 0;

    private int[] enemyUpgradeCosts = { 5, 10, 15 };
    private int[] healthUpgradeCosts = { 3, 6, 9 }; // cost per level. Test Values
    private int[] damageUpgradeCosts = { 3, 6, 9 }; // cost per level. Test Values


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between scenes
            SceneManager.sceneLoaded += SceneLoaded;//SceneLoaded() is called everytime we load a new scene.
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Upgrades_Scene" || scene.name == "Main_Menu_Scene")
        {
            if (scene.name == "Upgrades_Scene")
            {
                goobletText = GameObject.FindWithTag("GoobletText").GetComponent<TextMeshProUGUI>();
                UpdateGoobletDisplay();

            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void AddGooblets(int amount)
    {
        totalGooblets += amount;
        Debug.Log($"Gooblets: {totalGooblets}");
    }
    private void UpdateGoobletDisplay()
    {
        goobletText.text = $" Gooblets: {totalGooblets.ToString()}";
    }
    public void UpgradeHealth()
    {
        // Check if we can upgrade further
        if (healthUpgradeLevel >= healthUpgradeCosts.Length)
        {
            Debug.Log("Max health upgrades reached!");
            return;
        }

        int cost = healthUpgradeCosts[healthUpgradeLevel];//our upgrade level determines how much it costs

        if (totalGooblets >= cost)
        {
            totalGooblets -= cost;//spend gooblets
            extraHealth += 1;
            healthUpgradeLevel += 1;//upgrades our health level so we can purchase level 2
            UpdateGoobletDisplay();

            Debug.Log($"Purchased Health Upgrade {healthUpgradeLevel}! " +
                      $"Extra health: {extraHealth}, Gooblets left: {totalGooblets}");
        }
        else
        {
            Debug.Log("Not enough gooblets for next health upgrade!");
        }
    }
    public void UpgradeDamage()
    {
        // Check if we can upgrade further
        if (damageUpgradeLevel >= damageUpgradeCosts.Length)
        {
            Debug.Log("Max damage upgrades reached!");
            return;
        }

        int cost = damageUpgradeCosts[damageUpgradeLevel]; // cost based on current level

        if (totalGooblets >= cost)
        {
            totalGooblets -= cost; // spend gooblets
            extraDamage += 1;      // +1 damage per upgrade
            damageUpgradeLevel += 1; // go to next upgrade level
            UpdateGoobletDisplay();

            Debug.Log($"Purchased Damage Upgrade {damageUpgradeLevel}! " +
                      $"Extra damage: {extraDamage}, Gooblets left: {totalGooblets}");
        }
        else
        {
            Debug.Log("Not enough gooblets for next damage upgrade!");
        }



        /*cost = healthUpgradeCosts[healthUpgradeLevel];//our upgrade level determines how much it costs

        if (totalGooblets >= cost)
        {
            totalGooblets -= cost;//spend gooblets
            extraHealth += 1;
            healthUpgradeLevel += 1;//upgrades our health level so we can purchase level 2
            UpdateGoobletDisplay();

            Debug.Log($"Purchased Health Upgrade {healthUpgradeLevel}! " +
                      $"Extra health: {extraHealth}, Gooblets left: {totalGooblets}");
        }
        else
        {
            Debug.Log("Not enough gooblets for next health upgrade!");
        }*/
    }

    public void UpgradeEnemy()
    {
        if(enemyUpgradeLevel > enemyUpgradeCosts.Length)
        {
            Debug.Log("Max Enemy Upgrades Reached!");
            return;
            
        }

        int cost = enemyUpgradeCosts[enemyUpgradeLevel];

        if(totalGooblets >= cost)
        {
            totalGooblets -= cost;
            UpdateGoobletDisplay();
            enemyUpgradeLevel++;

            switch (enemyUpgradeLevel)
            {
                case 1:
                    extraEnemy1Damage++;
                    extraEnemy1Health++;
                    extraEnemy1GoobletDrop += 2;
                    break;
                case 2:
                    extraEnemy2Damage++; 
                    extraEnemy2Health++;
                    extraEnemy2GoobletDrop += 3;
                    break;
                case 3:
                    extraEnemy3Damage++;
                    extraEnemy3Health+=2;
                    extraEnemy3GoobletDrop += 5;
                    break;
            }
        }
    }

    
}
