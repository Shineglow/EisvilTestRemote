using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : IWeaponLogic
{
    private WeaponMono _weaponMono;
    private WeaponConfiguration _configuration;
    private GameObject _owner;
    private LayerMask _targetMask;

    private HashSet<DamageableComponent> _damagedThisFrame = new();

    public EWeapons Weapon => _configuration.Weapon;
    public WeaponMono WeaponMono => _weaponMono;

    public WeaponLogic(WeaponMono weaponMono, WeaponConfiguration configuration)
    {
        SetView(weaponMono);
        SetConfiguration(configuration);

        _weaponMono.WeaponHitTarget += OnHit;
    }

    public void SetView(WeaponMono weaponMono)
    {
        if (_weaponMono != null)
        {
            _weaponMono.WeaponHitTarget -= OnHit;
        }
        _weaponMono = weaponMono;
        _weaponMono.WeaponHitTarget += OnHit;
    }

    public void SetConfiguration(WeaponConfiguration configuration)
    {
        _configuration = configuration;
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

    public Transform GetTransform()
    {
        return _weaponMono.transform;
    }
}

public interface IWeaponLogic
{
    void Equip(GameObject owner, LayerMask targetMask);
    void Unequip();
    void SetWeaponAttackMode();
    void SetWeaponNormalMode();
    Transform GetTransform();
}