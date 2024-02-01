using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _coinTrigger;
    [SerializeField] private Slider _slider;

    private const string TakeHit = "takeDamage";

    private Animator _animator;

    private float _maximalHealth;
    private float _currentHealth;
    private float _healingEffect;
    private float _startHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _maximalHealth = 100;
        _slider.maxValue = _maximalHealth;
        _slider.value = _maximalHealth;
        _currentHealth = _maximalHealth;
        _healingEffect = 15;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _coinTrigger.Invoke();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out Potion potion))
        {
            _startHealth = _currentHealth;
            _currentHealth += _healingEffect;
            _slider.value = Mathf.MoveTowards(_startHealth, _currentHealth, Time.time);
            Destroy(collision.gameObject);
            CheckHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        _startHealth = _currentHealth;
        _animator.SetTrigger(TakeHit);
        _currentHealth -= damage;
        _slider.value = Mathf.MoveTowards(_startHealth, _currentHealth, Time.time);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        else if (_currentHealth > _maximalHealth) 
        {
            _currentHealth = _maximalHealth;
        }
    }
}
