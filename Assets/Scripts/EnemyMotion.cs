using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMotion : MonoBehaviour
{
    [SerializeField] private float _speed;

    private const string IsWalk = "isWalk";

    private SpriteRenderer _spriteRenderer;
    private Animator _enemyAnimator;

    private int _collisionCount;
    private int _countDivider;

    private bool _isWorking;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAnimator = GetComponent<Animator>();

        _collisionCount = 0;
        _countDivider = 2;
        _isWorking = true;

        _enemyAnimator.SetBool(IsWalk, true);

        StartCoroutine(Patroling());
    }
    public void StartPatroling()
    {
        StartCoroutine(Patroling());
    }

    public void StopPatroling()
    {
        StopCoroutine(Patroling());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PathPoint>(out PathPoint pathPoint))
        {
            _spriteRenderer.flipX = true;
            _collisionCount++;
            _speed *= -1;

            if (_collisionCount % _countDivider == 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }

    private IEnumerator Patroling()
    {
        while (_isWorking)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}
