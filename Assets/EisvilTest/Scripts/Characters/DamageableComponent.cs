using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageableComponent : MonoBehaviour
{
    public event Action<float> DamageInflicted;
    
    public void TakeDamage(float damage)
    {
        DamageInflicted?.Invoke(damage);
    }
}