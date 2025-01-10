using UnityEngine;

public class trash : MonoBehaviour
{
    public GameObject failedItem;
    public bool used;

    // Update is called once per frame
    void Update()
    {
        if (used)
        {
            Destroy(failedItem);
            used = false;
        }
    }
}
