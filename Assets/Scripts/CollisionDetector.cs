using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _player.AddMoney();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.TryGetComponent(out Potion potion))
        {
            _player.GetTreatment(potion.HealingEffect);
            Destroy(collision.gameObject);
            _player.RefreshHealthData();
        }
    }
}
