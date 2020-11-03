/*
 * Chris Smith
 * Prototype 4
 * Script for spawning enemies
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNum = 1;
    public GameObject powerupPrefab;
    public bool gameOver = false;
    public bool gameWon = false;
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNum);
        SpawnPowerup(1);
        Time.timeScale = 1f;
    }

    private void SpawnEnemyWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup(int numPowerups)
    {
        for (int i = 0; i < numPowerups; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            waveNum++;
            SpawnEnemyWave(waveNum);
            SpawnPowerup(1);
        }

        if (waveNum == 11)
        {
            gameWon = true;
            gameOver = true;
        }

        if (gameOver)
        {
            Time.timeScale = 0f;
            if (gameWon)
            {
                uiManager.gameWin.enabled = true;
            }
            else
            {
                uiManager.gameLose.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    
}
