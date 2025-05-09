using EisvilTest.Scripts.Triggers;
using UnityEngine;

namespace EisvilTest.Scripts
{
    public class BootstrapMain : MonoBehaviour
    {
        [SerializeField] private UniversalTrigger trigger;
        
        private void Awake()
        {
            // initialization here
            int mask = LayerMask.GetMask("Player");
            trigger.AddActionToBoth(OnUniversalTriggerEnter, OnUniversalTriggerExit, mask);
        }

        private void OnUniversalTriggerEnter()
        {
            Debug.Log("Enter");
        }
        
        private void OnUniversalTriggerExit()
        {
            Debug.Log("Exit");
        }
    }
}
