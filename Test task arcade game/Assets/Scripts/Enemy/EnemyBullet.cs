using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private int EnemyDamageBullet;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider collision) // тригер вражеской пули для нанесения урона игроку и уничтожения 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<Player>().TakeDamagePlayer(EnemyDamageBullet);
           Destroy(gameObject);            
        }
        if (collision.gameObject.CompareTag("Wall")) // уничтожение вражеской пули при касание стен
        {
            Destroy(gameObject);            
        }
    }
}
