using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject gameButtonsPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    [Header("Buttons")]
    public Button retryButton;
    public Button mainmenuButton;
    public Button playAgainButton;
    public Button menuButton;

    [Header("Game Variables")]
    public LaserSpawner laserSpawner;
    float _laserSpeed = 3;
    int _lasersToSpawn = 0;

    int _waves = 5;
    int _currentWave = 0;
    bool _canSpawnNextWave = true;
    float _timeBetweenWaves = 10f;
    float _timeUntilNextWave = 0f;

    public bool isGameOver;
    public bool isPlayerAlive;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isGameOver = false;
        isPlayerAlive = true;

        gameButtonsPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);

        retryButton.onClick.AddListener(Retry);
        mainmenuButton.onClick.AddListener(MainMenu);
        playAgainButton.onClick.AddListener(Retry);
        menuButton.onClick.AddListener(MainMenu);
       
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (_canSpawnNextWave && _currentWave < _waves)
            {
                _timeUntilNextWave -= Time.deltaTime;

                if (_timeUntilNextWave <= 0f)
                {
                    _canSpawnNextWave = false;
                    StartCoroutine(SpawnLaserWave(_currentWave));
                }
            }
            else if(_currentWave >= _waves && isPlayerAlive)
            {
                gameWinPanel.SetActive(true);
            }
        }
        else
        {
            gameButtonsPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    IEnumerator SpawnLaserWave(int waveIndex)
    {
        if (waveIndex >= _waves)
        {
            Debug.LogWarning($"Wave index {waveIndex} is out of range.");
            yield break;
        }

        int lasersToSpawnThisWave = LasersToSpawn(waveIndex);
        float laserSpeed = GetLaserSpeed(waveIndex);
        for (int i = 0; i < lasersToSpawnThisWave; i++)
        {
            yield return new WaitForSeconds(2);
            laserSpawner.SpawnLaser(laserSpeed);
        }

        yield return new WaitForSeconds(_timeBetweenWaves);

        _currentWave++;
        _canSpawnNextWave = true;
        _timeUntilNextWave = _timeBetweenWaves;
    }

    public float GetLaserSpeed(float waveIndex)
    {
        switch (waveIndex)
        {
            case 0:
                _laserSpeed += 0;
                break;
            case 1:
                _laserSpeed += 1;
                break;
            case 2:
                _laserSpeed += 1;
                break;
            case 3:
                _laserSpeed += 1;
                break;
            case 4:
                _laserSpeed += 1;
                break;
            default:
                Debug.LogWarning($"Wave index {waveIndex} is not handled in LasersToSpawn().");
                break;
        }
        return _laserSpeed;
    }
    int LasersToSpawn(int waveIndex)
    {
        int lasersSpawnNumbers = _lasersToSpawn; // Start with default lasers to spawn

        switch (waveIndex)
        {
            case 0:
                lasersSpawnNumbers += 5;
                break;
            case 1:
                lasersSpawnNumbers += 5;
                break;
            case 2:
                lasersSpawnNumbers += 5;
                break;
            case 3:
                lasersSpawnNumbers += 5;
                break;
            case 4:
                lasersSpawnNumbers += 5;
                break;
            default:
                Debug.LogWarning($"Wave index {waveIndex} is not handled in LasersToSpawn().");
                break;
        }
        return lasersSpawnNumbers;
    }

    public void Retry()
    {
        MainMenuManager.Instance.Retry();
    }

    public void MainMenu()
    {
        MainMenuManager.Instance.MainMenu();
    }
}