using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMotion : MonoBehaviour
{
    private const string IsWalk = "isWalk";

    [SerializeField] private CastSender _sender;
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Animator _enemyAnimator;

    private int _collisionCount;
    private int _countDivider;

    private bool _isPatrolling;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _collisionCount = 0;
        _countDivider = 2;
        _isPatrolling = true;

        _enemyAnimator.SetBool(IsWalk, true);
    }

    private void Update()
    {
        if (_isPatrolling == true) 
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnEnable()
    {
        _sender.PlayerIsFound += StopPatroling;
        _sender.PlayerIsNotFound += StartPatroling;
    }

    private void OnDisable()
    {
        _sender.PlayerIsFound -= StopPatroling;
        _sender.PlayerIsNotFound -= StartPatroling;
    }

    private void StartPatroling()
    {
        _isPatrolling = true;
    }

    private void StopPatroling()
    {
        _isPatrolling = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PathPoint pathPoint))
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
}
