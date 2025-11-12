using UnityEngine;
using System.Collections.Generic;
public class WaveManager : MonoBehaviour
{
    [Header("Inscribed")]
    public List<GameObject> enemiesList = new List<GameObject>();
    public float waveDurration;
    public float spawnRange = 10;
    public BoxCollider spawnArea;

    [Header("Dynamic")]
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    

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
                Vector3 randomPos = GetSpawnPos();
                Instantiate(enemiesToSpawn[0], randomPos,Quaternion.identity );
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

        if(enemiesToSpawn.Count == 0)
        {
            if (CheckForEnemies())
            {
                Debug.Log("Wave Cleared");
                currWave++;
                GenerateWave();
            }
        }
        
    }

    public void GenerateWave()
    {
        waveValue = currWave * 6;
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

    public Vector3 GetSpawnPos()
    {
       Vector3 center = spawnArea.center + spawnArea.transform.position;
       Vector3 size = spawnArea.size;

       float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
       float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
       float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

       return new Vector3(x, y, z);

        /*float xRand = Random.Range(-spawnRange, spawnRange) +transform.position.x;
        float zRand = Random.Range(-spawnRange, spawnRange) +transform.position.z;
        float yLoc = transform.position.y;

        spawnLocation.position = new Vector3(xRand, yLoc, zRand);*/

    }

    public bool CheckForEnemies()
    {
        //return true if no enemies returns false if there are enemies

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            return true;
        }
        return false;
    }

    
}
