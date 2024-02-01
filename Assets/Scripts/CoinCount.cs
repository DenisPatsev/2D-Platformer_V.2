using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class CoinCount : MonoBehaviour
{
    private TMP_Text _coinCount;
    private float _count;

    private void Start()
    {
        _count = 0;
        _coinCount = GetComponent<TMP_Text>();
        _coinCount.text = "Ñoins = " + _count;
    }

    public void AddCoins()
    {
        _count++;
        _coinCount.text = "Coins = " + _count;
    }
}
