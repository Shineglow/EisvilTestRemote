using UnityEngine;

public class BoxDamageable : MonoBehaviour
{
    public IObservableValueReadOnly<float> Durability;
    [SerializeField] private ObservableValue<float> _durability;
    [SerializeField] private DamageableComponent damageableComponent;

    private void Awake()
    {
        damageableComponent.DamageInflicted += OnDamageInflicted;
    }

    private void OnDamageInflicted(float damage)
    {
        _durability.Value -= damage;

        if(_durability.Value <= 0 )
        {
            Destroy(gameObject);
        }
    }
}