using UnityEngine;

public class pan : MonoBehaviour
{
    public GameObject spot;
    public GameObject PlacedItem;
    public bool isPlaced;

    public void Update()
    {
        if (isPlaced)
        {
            PlacedItem.TryGetComponent<Rigidbody>(out Rigidbody r);
        }
    }
}
