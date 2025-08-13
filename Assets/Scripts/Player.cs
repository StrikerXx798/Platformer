using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    public void CollectFruit()
    {
        Bag.AddFruit();
    }
}