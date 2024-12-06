using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private int speed = 5;
    [SerializeField] private Transform _enemyBulletPrefab;
    [SerializeField] private int _fallDamage = 20;    

    void Start() // нахождения игрока по тегу
    {         
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public void EnemyFire() // вражеский огонь по игроку
    {        
        Vector2 direction = (_player.position - transform.position).normalized;        
        Rigidbody rb = Instantiate(_enemyBulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();        
        rb.velocity = direction * speed;        
    }

    private void OnCollisionEnter2D(Collision2D collision) // Нанесение урона игроку при падение врага 
    {
        if(collision.gameObject.CompareTag("Points"))
        {
            _player.GetComponent<Player>().TakeDamagePlayer(_fallDamage);            
            gameObject.SetActive(false);
        }
    }
}
