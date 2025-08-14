using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialRotation = transform.rotation;
    }

    public void FlipSprite(bool flipCondition)
    {
        transform.rotation =
            flipCondition ? _initialRotation * Quaternion.Euler(0, 180, 0) : _initialRotation;
    }
}