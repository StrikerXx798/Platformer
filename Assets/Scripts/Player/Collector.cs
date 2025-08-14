using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _fruits;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollectFruit(other);
    }

    private void CollectFruit(Collider2D fruitCollider)
    {
        if (fruitCollider.TryGetComponent(out Fruit fruit))
        {
            _fruits++;
            fruit.Collect();
        }
    }
}