using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : IDisposable
{
    private readonly WeaponMono _weaponMono;
    private readonly WeaponConfiguration _configuration;
    private GameObject _owner;
    private LayerMask _targetMask;

    private HashSet<DamageableComponent> _damagedThisFrame = new();

    public WeaponLogic(WeaponMono weaponMono, WeaponConfiguration configuration)
    {
        _weaponMono = weaponMono;
        _configuration = configuration;

        _weaponMono.WeaponHitTarget += OnHit;
    }

    public void Equip(GameObject owner, LayerMask targetMask)
    {
        _owner = owner;
        _weaponMono.SetLayerMask(_targetMask = targetMask);
    }

    public void Unequip()
    {
        _owner = null;
        _weaponMono.SetLayerMask(_targetMask = 0);
    }

    private void OnHit(GameObject target)
    {
        if (_owner == null || target == _owner)
            return;

        if (((1 << target.layer) & _targetMask) == 0)
            return;

        Debug.Log($"{_owner.name} hit {target.name} with {_configuration.Weapon}");

        if (target.TryGetComponent(out DamageableComponent damageable) && !_damagedThisFrame.Contains(damageable))
        {
            damageable.TakeDamage(_configuration.Damage);
            _damagedThisFrame.Add(damageable);
        }
    }

    public void SetWeaponAttackMode() => _weaponMono.SetCollisionEnabled(true);
    public void SetWeaponNormalMode()
    {
        _weaponMono.SetCollisionEnabled(false);
        _damagedThisFrame.Clear();
    }

    public void Dispose()
    {
        Unequip();
        _weaponMono.WeaponHitTarget -= OnHit;
    }

    internal Transform GetTransform()
    {
        return _weaponMono.transform;
    }
}