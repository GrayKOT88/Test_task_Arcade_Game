using TMPro;
using UnityEngine;

public class BlockSettings : MonoBehaviour
{
    [SerializeField] private int _blockHP;
    [SerializeField] private int _blockArmor;
    [SerializeField] private TextMeshProUGUI _currentHP;
    private int _hitDamage = 1;

    private void Start()
    {
        _currentHP.text = _blockHP.ToString();
    }

    private void TakeDamage(int damage) // урон по блоку
    {        
        if (_blockArmor <= 0)
        {
            _blockHP -= damage;
            _currentHP.text = _blockHP.ToString();
            if (_blockHP <= 0)
            {
                gameObject.SetActive(false);
            }            
        }
        _blockArmor -= damage;        
    }

    private void OnCollisionEnter2D(Collision2D collision) // касание пули
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(_hitDamage);
        }
    }
}
