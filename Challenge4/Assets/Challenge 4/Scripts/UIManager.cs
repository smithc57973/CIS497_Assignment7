/*
 * Chris Smith
 * Challenge 4
 * Script for controlling UI elements
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text waves;
    public Text enemies;
    public Text gameWin;
    public Text gameLose;
    public GameObject tutPanel;
    public SpawnManagerX spawnManager;

    void Awake()
    {
        gameWin.enabled = false;
        gameLose.enabled = false;
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        waves.text = "Wave: " + (spawnManager.waveCount - 1);
        enemies.text = "Enemies Left: " + spawnManager.enemyCount;
    }
}


//To do
//The enemies never get more difficult – The enemies’ speed should increase in speed by a small amount with every new wave 
//    Hint - You’ll need to track and increase the enemy speed in SpawnManagerX.cs.Then in EnemyX.cs, reference that speed variable and set it in Start().

//Add loss condition: all enemies in the current wave get through the player goal.

    