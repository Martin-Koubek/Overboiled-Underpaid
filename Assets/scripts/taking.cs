using UnityEngine;

public class taking : MonoBehaviour
{
    public GameObject hand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            RaycastHit hit;
            if(hit && CompareTag())
        }
    }
}
