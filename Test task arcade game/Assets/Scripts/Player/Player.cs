using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{    
    [SerializeField] private int _shotsLimit = 5; //лимит выстрелов на один ход
    private List <Transform> poins = new List<Transform>(); // точки позиций
    private int _numberOfShots;
    private int _previousNumber = 2;

    [SerializeField] private int _playerHP = 100;
    [SerializeField] private TextMeshProUGUI _textPlayerHP;

    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private bool _isGameActive; // игра активна

    [SerializeField] private PlayerBulletPool _bulletPool; // пул патронов

    [SerializeField] private float _cooldownWindow = 0.1f;
    private float _nextTimeToShoot;
    private float _timeToWait = 2f;

    private void Start() // поиск точек и добовление их в коллекцию
    {           
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform t in pointsObject)
        {
            poins.Add(t);
        }
        _textPlayerHP.text = _playerHP.ToString();        
    }
    void FixedUpdate()
    {
        PlayerFire();
    }

    private void PlayerFire() // выстрел игрока
    {
        if (Input.GetMouseButton(0) && Time.time > _nextTimeToShoot && _isGameActive)        
        {   
            _bulletPool.ShootGun();
            CheckRound();            
        }
    }
    
    private void CheckRound() // проверка раунда
    {
        _numberOfShots++;
        if (_numberOfShots >= _shotsLimit)
        {
            Teleportation();
            EnemysFiers();
            _numberOfShots = 0;
            _nextTimeToShoot = Time.time + _timeToWait;
        }
        else
        {
            _nextTimeToShoot = Time.time + _cooldownWindow;
        }        
    }

    private void Teleportation() // перенос игрока в рандомную точку на платформе, не повторяя текущей точки
    {
        int randomNumber = Random.Range(0, 5);
        if (randomNumber == _previousNumber)
        {
            Teleportation();
        }
        else
        {
            _previousNumber = randomNumber;
            transform.position = poins[randomNumber].position;            
        }        
    }

    private void EnemysFiers() // Вражеский выстрел 
    {
        Enemy[] objectEnemy = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in objectEnemy)
        {
            enemy.EnemyFire();
        }
    }

    public void TakeDamagePlayer(int damage) // принятие урона игроком
    {
        _playerHP -= damage;
        _textPlayerHP.text = _playerHP.ToString();
        if( _playerHP <= 0 )
        {
            GameOver();
        }
    }

    private void GameOver() // конец игры
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _isGameActive = false;        
    }

    public void GameStart() // активация игры
    {
        _isGameActive = true;
    }
}
