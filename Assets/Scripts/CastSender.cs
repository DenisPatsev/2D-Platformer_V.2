using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class CastSender : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _enemyRigidbody;
    [SerializeField] private ContactFilter2D _filter;

    private RaycastHit2D[] _rightCastResults = new RaycastHit2D[1];
    private RaycastHit2D[] _leftCastResults = new RaycastHit2D[1];

    private SpriteRenderer _renderer;

    private float _castDistance;

    public event UnityAction PlayerIsFound;
    public event UnityAction PlayerIsNotFound;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _castDistance = 10;
    }

    private void Update()
    {
        SendRigidbodyCast(transform.right, _rightCastResults, 2);
        SendRigidbodyCast(transform.right * -1, _leftCastResults, -2);
    }

    private void SendRigidbodyCast(Vector3 direction, RaycastHit2D[] results, int speed)
    {
        var collisionCount = _enemyRigidbody.Cast(direction, _filter, results, _castDistance);

        if (collisionCount > 0)
        {
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] == true && results[i].collider.TryGetComponent(out Player player))
                {
                    PlayerIsFound?.Invoke();
                    float distance = results[i].transform.position.x - transform.position.x;

                    if (distance < 0)
                    {
                        _renderer.flipX = true;
                    }
                    else
                    {
                        _renderer.flipX = false;
                    }

                    transform.Translate(Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    PlayerIsNotFound?.Invoke();
                }
            }
        }
    }
}
