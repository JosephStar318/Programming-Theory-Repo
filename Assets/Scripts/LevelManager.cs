using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public static LevelManager Instance;
    public GameObject player;
    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI killCountText;
    private int level = 1;
    private int randomIndex;
    private float spawnRange = 10;
    public bool isGameOver = true;
    public int killCount = 0;

    private void Start()
    {
        Time.timeScale = 0;
        Instance = this;
    }
    private void SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);
            randomIndex = Random.Range(0, enemies.Count);
            Vector3 randomPos = new Vector3(randomX, 3, randomZ);
            Instantiate(enemies[randomIndex], randomPos, enemies[randomIndex].transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            if (FindObjectsOfType<Enemy>().Length < 1)
            {
                Debug.Log("Next Level...");
                level++;
                SpawnEnemies(level);
            }
            if (player.transform.position.y < -10)
            {
                Debug.Log("Game Over");
                isGameOver = true;
                EndGame();
            }
        }
    }
    public void StartGame()
    {
        Cursor.visible = false;
        startScreen.SetActive(false);
        SpawnEnemies(level);
        isGameOver = false;
        Time.timeScale = 1;
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame()
    {
        Cursor.visible = true;
        killCountText.SetText("Kill Count: " + killCount.ToString());
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
