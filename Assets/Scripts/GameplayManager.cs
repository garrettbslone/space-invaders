using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    public GameObject shield, enemyParent; 
    public GameObject[] enemies;

    private TMP_Text _scoreText, _highScoreText;
    private List<GameObject> _shields;
    private List<List<GameObject>> _enemies;
    private GameObject _player, _manager;
    private Transform _enemyBase;
    private int _score = 0, _highScore, _direction = 1, _enemiesCnt = 20;
    private float _speed = 1f, _overtm = 1f;
    private float _elapsed = 0f, _nextEnemyShot, _elapsedSinceShot = 0f;
    private readonly Vector2 _enemyShotRange = new Vector2(0.7f, 2.7f);
    private bool _switching = false, _over = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
        _highScoreText = GameObject.Find("High Score Text").GetComponent<TMP_Text>();
        _player = GameObject.Find("Player");
        _player.GetComponent<PlayerController>().onShot = PlayerShot;
        _enemyBase = enemyParent.transform;
        _highScore = PlayerPrefs.GetInt("highScore", 0);
        _nextEnemyShot = Random.Range(_enemyShotRange.x, _enemyShotRange.y);
        _manager = GameObject.Find("GameManagerObj");
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsed += Time.deltaTime;
        _elapsedSinceShot += Time.deltaTime;

        if (_elapsed >= _speed)
        {
            _elapsed = 0f;
            Vector3 pos = enemyParent.transform.position;

            if (_switching || Mathf.Abs(enemyParent.transform.position.x) < 6.5f)
            {
                _switching = false;
                pos += Vector3.right * 0.5f * _direction;
            }
            else
            {
                    _direction *= -1;
                    pos += Vector3.down * 0.5f;
                    _switching = true;
            }

            enemyParent.transform.position = pos;
        }

        if (_elapsedSinceShot >= _nextEnemyShot)
        {
            var r = Random.Range(0, 4);
            var c = Random.Range(0, _enemies[r].Count);
            _enemies[r][c].GetComponent<EnemyController>().Fire();
            _nextEnemyShot = Random.Range(_enemyShotRange.x, _enemyShotRange.y);
            _elapsedSinceShot = 0f;
        }

        if (_over)
        {
            _overtm -= Time.deltaTime;
            if (_overtm <= 0f)
            {
                _manager.GetComponent<GameManager>().LoadScene("CreditsScene");
            }
        }
    }

    void PlayerShot(int livesRemaining, GameObject bullet)
    {
        if (livesRemaining == 0)
        {
            Debug.Log("Game over!");
            _over = true;
            // _manager.GetComponent<GameManager>().LoadScene("CreditsScene");
        }

        Destroy(bullet);
    }

    void EnemyShot(GameObject enemy, GameObject bullet, int points)
    {
        _enemiesCnt--;
        _score += points;

        if (_enemiesCnt == 0)
        {
            _manager.GetComponent<GameManager>().LoadScene("CreditsScene");
        }

        _scoreText.text = $"Score\n{_score:0000}";

        List<GameObject> r = null;
        
        foreach (var row in _enemies)
        {
            foreach (var e in row)
            {
                if (e == enemy)
                {
                    r = row;
                }
            }
        }

        r?.Remove(enemy);
        
        if (r is { Count: 0 })
        {
            _enemies.Remove(r);
        }
        
        Destroy(bullet);
    }

    void RestartGame()
    {
        if (_score > _highScore)
        {
            PlayerPrefs.SetInt("highScore", _score);
            _highScore = _score;
            _highScoreText.text = $"Hi-Score\n{_highScore:0000}";
        }

        _score = 0;
        SpawnEnemies();
        SpawnShields();
        _player.GetComponent<PlayerController>().ResetPlayer();
    }

    void SpawnShields()
    {
        _shields = new List<GameObject>();
        
        for (int i = 0; i < 4; i++)
        {
            _shields.Add(Instantiate(shield, new Vector3(-4.8f + 3.2f * i, -2.5f), Quaternion.identity));
        }
    }

    void SpawnEnemies()
    {
        enemyParent.transform.position = _enemyBase.position;
        
        _enemies = new List<List<GameObject>>();
        _enemiesCnt = 20;

        for (int i = 0; i < 4; i++)
        {
            _enemies.Add(new List<GameObject>());
            
            for (int j = 0; j < 5; j++)
            {
                _enemies[i].Add(Instantiate(enemies[i], new Vector3(-2.8f + 1.4f * j, 4.5f - i * 1.2f, 0), Quaternion.identity));
                _enemies[i][j].transform.parent = enemyParent.transform;
                _enemies[i][j].GetComponent<EnemyController>().onEnemyHit = EnemyShot;
            }
        }
    }
}
