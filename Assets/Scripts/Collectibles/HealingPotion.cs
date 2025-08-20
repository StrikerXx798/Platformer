using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private float _healValue;

    public float Collect()
    {
        Destroy(gameObject);

        return _healValue;
    }
}