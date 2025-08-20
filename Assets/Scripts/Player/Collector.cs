using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _fruits;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CollectFruit(other);
        CollectHealingPotion(other);
    }

    private void CollectFruit(Collider2D fruitCollider)
    {
        if (fruitCollider.TryGetComponent(out Fruit fruit))
        {
            _fruits++;
            fruit.Collect();
        }
    }

    private void CollectHealingPotion(Collider2D potionCollider)
    {
        if (potionCollider.TryGetComponent(out HealingPotion healingPotion))
        {
            var healValue = healingPotion.Collect();

            if (TryGetComponent(out Health health))
            {
                health.Heal(healValue);
            }
        }
    }
}