using UnityEngine;

public class Dummy : MonoBehaviour 
{
    [SerializeField] private DamageableComponent _damageableComponent;

    private void Awake()
    {
        _damageableComponent.DamageInflicted += OnDamageInflicted;
    }

    private void OnDamageInflicted(float damage)
    {
        Debug.Log($"Dummy {gameObject.name} take {damage} damage");
    }
}