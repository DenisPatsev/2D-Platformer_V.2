using System.Collections;
using UnityEngine;

public class HealingSpawner : MonoBehaviour
{
    [SerializeField] private Potion _potion;
    [SerializeField] private PotionSpawner[] _generators;

    private Transform _spawnerTransform;

    private int _minimalPointNumber;
    private int _maximalPointNumber;
    private bool _isWorking;
    private float _delay;

    private void Start()
    {
        _minimalPointNumber = 0;
        _maximalPointNumber = _generators.Length;
        _delay = 10;

        _isWorking = true;

        StartCoroutine(Generate(_delay));
    }

    private IEnumerator Generate(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (_isWorking)
        {
            int index = Random.Range(_minimalPointNumber, _maximalPointNumber);

            for (int i = 0; i < _generators.Length; i++)
            {
                if (i == index)
                {
                    _spawnerTransform = _generators[i].transform;
                    GeneratePotion(_spawnerTransform.position);
                }
            }

            yield return wait;
        }
    }

    private void GeneratePotion(Vector3 position)
    {
        Instantiate(_potion, position, Quaternion.identity);
    }
}
