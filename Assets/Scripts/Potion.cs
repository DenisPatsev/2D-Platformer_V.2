using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float _healingEffect;

    public float HealingEffect => _healingEffect;
}
