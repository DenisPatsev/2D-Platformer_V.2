using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAttack : MonoBehaviour
{
    private const string Attack = "attack";

    [SerializeField] private float _damage;
    [SerializeField] private float _vampireEffect;
    [SerializeField] private float _distanceRatio;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private Player _player;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _body;

    private bool _vampirismIsActive;

    private float _distance;
    private float _startDistance;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startDistance = 1.5f;
        _distance = _startDistance;
        _vampirismIsActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.SetTrigger(Attack);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _vampirismIsActive = true;
            _distance = _startDistance * _distanceRatio;
            TryAttack();
        }
        else
        {
            _vampirismIsActive = false;
        }
    }

    public void TryAttack()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];

        if (_spriteRenderer.flipX == false)
        {
            _body.Cast(_distanceChecker.right, hits, _distance);
        }
        else
        {
            _body.Cast(_distanceChecker.right * -1, hits, _distance);
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (_vampirismIsActive == true)
                {
                    enemy.TakeDamage(_vampireEffect);
                    _player.GetTreatment(_vampireEffect);
                }
                else
                {
                    enemy.TakeDamage(_damage);
                }
            }
        }
    }
}
