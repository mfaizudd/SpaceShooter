using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject hazard;
    [SerializeField]
    private Vector3 spawnValues;
    [SerializeField]
    private float hazardWait;
    [SerializeField]
    private float waveWait;
    [SerializeField]
    private float startWait;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text restartText;
    [SerializeField]
    private Text gameOverText;

    private static GameController instance;
    private int score;

    public static GameController Instance
    {
        get
        {
            if(instance==null)
            {
                Debug.LogError("Game controller isn't instantiated");
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        private set
        {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }

    private void Awake()
    {
        instance = this;
        Score = 0;
    }

    private void Start()
    {
        StartCoroutine(SpawnHazard());
    }

    IEnumerator SpawnHazard()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(hazardWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    public void GameOver()
    {
        restartText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

}
