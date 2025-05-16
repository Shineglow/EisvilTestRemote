using System;
using UnityEngine;
using UnityEngine.Animations;

namespace EisvilTest.Scripts.General
{
    [RequireComponent(typeof(Camera),typeof(PositionConstraint))]
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] public Camera CameraMain { get; private set; }
        [SerializeField] private PositionConstraint positionConstraint;

        private void Awake()
        {
            positionConstraint.AddSource(new ConstraintSource());
        }

        public void SetTarget(Transform target)
        {
            positionConstraint.SetSource(0, new()
            {
                sourceTransform = target,
                weight = 1,
            });
        }

        public void SetOffset(Vector3 offset)
        {
            positionConstraint.translationOffset = offset;
        }
    }
}
