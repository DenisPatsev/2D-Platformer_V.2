using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private Spawner[] _generators;
    private Transform _spawnerTransform;

    private int minimalPointNumber;
    private int maximalPointNumber;
    private bool _isWorking;
    private float _secondNumber;

    private void Start()
    {
        _generators = FindObjectsOfType<Spawner>();

        minimalPointNumber = 0;
        maximalPointNumber = _generators.Length;
        _secondNumber = 2;

        _isWorking = true;

        StartCoroutine(Generate(_secondNumber));
    }

    private IEnumerator Generate(float secondsNumber)
    {
        var wait = new WaitForSeconds(secondsNumber);

        while (_isWorking)
        {
            int index = Random.Range(minimalPointNumber, maximalPointNumber);

            for (int i = 0; i < _generators.Length; i++)
            {
                if (i == index)
                {
                    _spawnerTransform = _generators[i].transform;
                    GenerateCoin(_spawnerTransform.position);
                }
            }

            yield return wait;
        }
    }

    private void GenerateCoin(Vector3 position)
    {
        Instantiate(_coin, position, Quaternion.identity);
    }
}
