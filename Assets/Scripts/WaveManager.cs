using UnityEngine;
using System.Collections.Generic;
public class WaveManager : MonoBehaviour
{
    [Header("Inscribed")]
    public List<GameObject> enemiesList = new List<GameObject>();
    public float waveDurration;
    public float spawnRange = 10;

    [Header("Dynamic")]
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public Transform spawnLocation;

    private float spawnInterval;
    private float spawnTimer;
    private float waveTimer;
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0) 
            {
                
                Instantiate(enemiesToSpawn[0], spawnLocation.position,Quaternion.identity );
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval; 
                
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDurration / enemiesToSpawn.Count;
        waveTimer = waveDurration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyID = Random.Range(0, enemiesList.Count);
            GameObject chosenEnemy = enemiesList[randEnemyID];
            EnemyMove enemyScript = chosenEnemy.GetComponent<EnemyMove>();
            int randEnemyCost = enemyScript.spawnCost;
            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(chosenEnemy);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    /*public void GetSpawnPos()
    {
       

        float xRand = Random.Range(-spawnRange, spawnRange) +transform.position.x;
        float zRand = Random.Range(-spawnRange, spawnRange) +transform.position.z;
        float yLoc = transform.position.y;

        spawnLocation.position = new Vector3(xRand, yLoc, zRand);

    }*/
}
