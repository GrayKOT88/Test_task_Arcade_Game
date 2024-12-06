using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletPool : MonoBehaviour
{
    [SerializeField] private PlayerBullet _playerBulletPrefab; // префаб пули игрока
    [SerializeField] private float _muzzleVelocity = 5; //начальная скорость пули
    [SerializeField] private Transform _muzzlePosition; // точка выстрела

    //[SerializeField] private float _cooldownWindow = 0.1f;
    //private float _nextTimeToShoot;

    private IObjectPool<PlayerBullet> _objectPool;

    [SerializeField] private bool _collectionCheck = true;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 20;

    #region Pool of Bullets
    private void Awake()
    {
        _objectPool = new ObjectPool<PlayerBullet>(CreateProjectile, OnGetFromPool, OnReleaseToPool,
            OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    private PlayerBullet CreateProjectile() //вызывается при создании элемента для заполнения пула объектов
    {
        PlayerBullet projectileInstance = Instantiate(_playerBulletPrefab);
        projectileInstance.ObjectPool = _objectPool;
        return projectileInstance;
    }

    private void OnGetFromPool(PlayerBullet pooledObject) //вызывается при извлечении следующего элемента из пула объектов
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(PlayerBullet pooledObject) //вызывается при возврате элемента в пул объектов
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(PlayerBullet pooledObject) //вызывается, когда мы превышаем максимальное количество элементов в пуле
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion

    #region Shooting
    public void ShootGun() // Берётся пуля из пула, запускается в сторону нажатие мыши, запускается метотд декативации пули
    {
        if (/*Time.time > _nextTimeToShoot &&*/ _objectPool != null)
        {
            PlayerBullet bulletObject = _objectPool.Get();
            if (bulletObject == null)
            {
                return;
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector2 direction = (mousePos - transform.position).normalized;            
            bulletObject.transform.SetPositionAndRotation(_muzzlePosition.position, _muzzlePosition.rotation);
            bulletObject.GetComponent<Rigidbody2D>().velocity = direction * _muzzleVelocity;           
            bulletObject.Deactivate();
            //_nextTimeToShoot = Time.time + _cooldownWindow;
        }
    }
    #endregion
}
