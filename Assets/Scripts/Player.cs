using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    private const string TakeHit = "takeDamage";

    private Animator _animator;

    private float _maximalHealth;
    private float _currentHealth;
    private float _money;

    public event UnityAction DamageTaked;
    public event UnityAction TreatmentAdded;
    public event UnityAction CoinAdded;

    public float CurrentHealth => _currentHealth;
    public float MaximalHealth => _maximalHealth;
    public float Money => _money;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _maximalHealth = 100;
        _currentHealth = _maximalHealth;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger(TakeHit);
        _currentHealth -= damage;

        DamageTaked?.Invoke();
        RefreshHealthData();
    }

    public void GetTreatment(float healingEffect)
    {
        _currentHealth += healingEffect;

        TreatmentAdded?.Invoke();
    }

    public void AddMoney()
    {
        _money++;

        CoinAdded?.Invoke();
    }

    public void RefreshHealthData()
    {
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Dead();
        }
        else if (_currentHealth > _maximalHealth)
        {
            _currentHealth = _maximalHealth;
        }
    }
}
