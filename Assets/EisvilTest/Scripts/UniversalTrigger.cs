using System;
using System.Collections.Generic;
using UnityEngine;

namespace EisvilTest.Scripts.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class UniversalTrigger : MonoBehaviour
    {
        [SerializeField] private new Collider collider;

        private List<ActionData> triggerEnterActions = new();
        private List<ActionData> triggerExitActions = new();

        private void Awake()
        {
            collider ??= GetComponent<Collider>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            InvokeActions(triggerEnterActions, other);
        }

        private void OnTriggerExit(Collider other)
        {
            InvokeActions(triggerExitActions, other);
        }
        
        private void InvokeActions(List<ActionData> collection,Collider other)
        {
            foreach (var actionData in collection)
            {
                if (((1 << other.gameObject.layer) & actionData.layerMask) != 0
                    && (actionData.predicate == null || actionData.predicate(other.gameObject)))
                {
                    actionData.action(other.gameObject);
                }
            }
        }
        
        public void AddActionToTriggerEnter(Action<GameObject> act, int layerMask = -1, Func<GameObject, bool> predicate = null)
        {
            AddDataToCollection(triggerEnterActions, act, layerMask, predicate);
        }

        public void AddActionToTriggerExit(Action<GameObject> act, int layerMask = -1, Func<GameObject, bool> predicate = null)
        {
            AddDataToCollection(triggerExitActions, act, layerMask, predicate);
        }

        public void AddActionToBoth(Action<GameObject> actEnter, Action<GameObject> actExit, int layerMask = -1, Func<GameObject, bool> predicate = null)
        {
            AddActionToTriggerEnter(actEnter, layerMask, predicate);
            AddActionToTriggerExit(actExit, layerMask, predicate);
        }

        private void AddDataToCollection(List<ActionData> collection, Action<GameObject> act, int layerMask, Func<GameObject, bool> predicate)
        {
            collection.Add(new ActionData()
            {
                action = act,
                layerMask = layerMask,
                predicate = predicate,
            });
        }

        public void RemoveActionFromTriggerEnter(Action<GameObject> act)
        {
            if (RemoveActionFromCollection(triggerEnterActions, act)) return;

            Debug.LogWarning($"Attempts to delete an object that is not in the collection {nameof(triggerEnterActions)}.");
        }

        public void RemoveActionFromTriggerExit(Action<GameObject> act)
        {
            if (RemoveActionFromCollection(triggerExitActions, act)) return;

            Debug.LogWarning($"Attempts to delete an object that is not in the collection {nameof(triggerExitActions)}.");
        }
        
        public void RemoveActionFromBoth(Action<GameObject> actEnter, Action<GameObject> actExit)
        {
            RemoveActionFromTriggerEnter(actEnter);
            RemoveActionFromTriggerExit(actExit);
        }
        
        private bool RemoveActionFromCollection(List<ActionData> collection, Action<GameObject> act)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].action != act) continue;
                collection[i] = collection[^1];
                collection.RemoveAt(collection.Count - 1);
                return true;
            }

            return false;
        }
    }

    public struct ActionData
    {
        public Action<GameObject> action;
        public LayerMask layerMask;
        public Func<GameObject, bool> predicate;
    }
}