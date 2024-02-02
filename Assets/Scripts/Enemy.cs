using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    private const string TakeHit = "takeDamage";

    private Animator _enemyAnimator;

    private float _maxHealth;
    private float _currentHealth;

    public event UnityAction DamageTaked;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _enemyAnimator.SetTrigger(TakeHit);
        _currentHealth -= damage;
        DamageTaked?.Invoke();

        if (_currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
