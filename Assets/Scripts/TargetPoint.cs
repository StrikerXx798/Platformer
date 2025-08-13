using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TargetPoint : MonoBehaviour
{
    public event Action Reached;

    private void OnTriggerEnter2D(Collider2D _)
    {
        Reached?.Invoke();
    }
}