using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour
{
    private int _touchCount = 0; //  количества касаний

    [SerializeField] private float timeoutDelay = 3f;
    private IObjectPool<PlayerBullet> objectPool;
    public IObjectPool<PlayerBullet> ObjectPool { set => objectPool = value; } // общедоступное свойство, позволяющее снаряду ссылаться на свой пул объектов
    public void Deactivate() // запуск корутины с возратом пули в пул через промежуток времени
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D rB = GetComponent<Rigidbody2D>();
        rB.velocity = new Vector2(0,0);        
        objectPool.Release(this);       // возрат в пул
        _touchCount = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision) // контроль за количеством рекашета пули   
    {
        if (_touchCount < 5) {
            _touchCount++;
        }
        else {            
            objectPool.Release(this);
            _touchCount = 0;
        }
    }
}
