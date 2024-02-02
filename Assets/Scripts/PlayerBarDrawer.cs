using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarDrawer : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private Player _player;
    [SerializeField] private float _changeValueRate;

    private void Start()
    {
        _bar.maxValue = _player.MaximalHealth;
        _bar.value = _player.CurrentHealth;
    }

    private void OnEnable()
    {
        _player.DamageTaked += ChangeBarValue;
        _player.TreatmentAdded += ChangeBarValue;
    }

    private void OnDisable()
    {
        _player.DamageTaked -= ChangeBarValue;
        _player.TreatmentAdded -= ChangeBarValue;
    }

    private void ChangeBarValue()
    {
        StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        var wait = new WaitForEndOfFrame();

        while (_bar.value != _player.CurrentHealth)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, _player.CurrentHealth, Time.deltaTime * _changeValueRate);
            yield return wait;
        }
    }
}
