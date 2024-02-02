using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TMP_Text _coins;

    private void Awake()
    {
        _coins = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _coins.text = "Ñoins = " + _player.Money;
    }

    private void OnEnable()
    {
        _player.CoinAdded += RefreshCoinData;
    }

    private void OnDisable()
    {
        _player.CoinAdded -= RefreshCoinData;
    }

    private void RefreshCoinData()
    {
        _coins.text = "Coins = " + _player.Money;
    }
}
