using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage;

    private const string Attack = "attack";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _distance;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _distance = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _animator.SetTrigger(Attack);
        }
    }
   
    public void TryAttack()
    {
        RaycastHit2D[] hits;

        if (_spriteRenderer.flipX == false)
        {
            hits = Physics2D.RaycastAll(transform.position, transform.right, _distance);
        }
        else
        {
            hits = Physics2D.RaycastAll(transform.position, transform.right * -1, _distance);
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
            }
        }
    }
}
