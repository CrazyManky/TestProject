using System;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;

public class ShotingZone : MonoBehaviour
{
    public event Action<Transform> OnEnterZone;
    public event Action OnExitZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoveObject moveObject))
        {
            OnEnterZone?.Invoke(moveObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out MoveObject moveObject))
        {
            OnExitZone?.Invoke();
        }
    }
}