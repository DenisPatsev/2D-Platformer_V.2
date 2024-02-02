using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _generators;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;

    private int _minimalPointNumber;
    private int _maximalPointNumber;
    private bool _isWorking;

    private void Start()
    {
        _minimalPointNumber = 0;
        _maximalPointNumber = _generators.Length;

        _isWorking = true;

        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isWorking)
        {
            GenerateItem();

            yield return wait;
        }
    }

    private void GenerateItem()
    {
        int index = Random.Range(_minimalPointNumber, _maximalPointNumber);

        Instantiate(_prefab, _generators[index].transform.position, Quaternion.identity);
    }
}
