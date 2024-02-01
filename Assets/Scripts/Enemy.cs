using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    private const string TakeHit = "takeDamage";

    [SerializeField] private Slider _healthBar;

    private Animator _enemyAnimator;

    private float _health;
    private float _startHealth;

    private void Start()
    {
        _enemyAnimator = gameObject.GetComponent<Animator>();
        _health = 100;
        _healthBar.maxValue = _health;
        _healthBar.value = _health;
        Debug.Log("Healthbar value: " + _healthBar.value);
    }

    public void TakeDamage(float damage)
    {
        _startHealth = _health;
        _enemyAnimator.SetTrigger(TakeHit);
        _health -= damage;
        _healthBar.value = Mathf.MoveTowards(_startHealth, _health, Time.time);
        Debug.Log("new Healthbar value: " + _healthBar.value);

        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }
}
