using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Data")]
    public int totalGooblets;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between scenes
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }

    public void AddGooblets(int amount)
    {
        totalGooblets += amount;
        Debug.Log($"Gooblets: {totalGooblets}");
    }

    public void SpendGooblets(int amount)
    {
        totalGooblets = Mathf.Max(totalGooblets - amount, 0);
        Debug.Log($"Gooblets left: {totalGooblets}");
    }
}
