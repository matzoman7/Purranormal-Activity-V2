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
        SceneManager.LoadScene("Game_Scene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu_Scene");
    }

    public void LevelUpgrade1()
    {

    }

    public void LevelUpgrade2()
    {

    }

    public void LevelUpgrade3()
    {

    }

    public void PlayerUpgrade1()
    {

    }

    public void PlayerUpgrade2()
    {

    }

    public void PlayerUpgrade3()
    {

    }


    public void EnemyUpgrade1()
    {
        //Acess enemy 1 script 
        
        //increase base health by 2

    }

    public void EnemyUpgrade2()
    {
        //Acess enemy  2 script 
        
        //increase base health by 2
    }

    public void EnemyUpgrade3()
    {
        //Acess enemy script 
        //increase enemy 3 drop
        //increase base health by 2
    }
}
