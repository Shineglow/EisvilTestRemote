using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Weapon
{
    public static class WeaponAnimations
    {
        public static Func<Transform, Transform, Transform, CancellationToken, UniTask> SwipeAnimationPattern(float hitAngle, float attackTime)
        {
             return async (weaponKeeper, distantPoint, self, token) =>
                {
                    float angleTraveled = 0f;
                    float anglePerSecond = hitAngle / attackTime;
                    float startAngleY = weaponKeeper.localEulerAngles.y;

                    while (angleTraveled < hitAngle)
                    {
                        token.ThrowIfCancellationRequested();

                        float step = anglePerSecond * Time.deltaTime;
                        step = Mathf.Min(step, hitAngle - angleTraveled);
                        angleTraveled += step;
                        weaponKeeper.localRotation = Quaternion.Euler(0f, startAngleY - angleTraveled, 0f);

                        await UniTask.Yield();
                    }
                };
        }
    }
}