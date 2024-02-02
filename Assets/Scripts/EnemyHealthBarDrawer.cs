using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarDrawer : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _changeValueRate;

    private void Start()
    {
        _bar.maxValue = _enemy.MaxHealth;
        _bar.value = _enemy.CurrentHealth;
    }

    private void OnEnable()
    {
        _enemy.DamageTaked += ChangeBarValue;
    }

    private void OnDisable()
    {
        _enemy.DamageTaked -= ChangeBarValue;
    }

    private void ChangeBarValue()
    {
        StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        var wait = new WaitForEndOfFrame();

        while (_bar.value != _enemy.CurrentHealth)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _enemy.CurrentHealth, Time.deltaTime * _changeValueRate);
            yield return wait;
        }
    }
}
