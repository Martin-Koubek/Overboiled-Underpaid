using Unity.VisualScripting;
using UnityEngine;

public class clip : MonoBehaviour
{
    public Transform hold;
    public GameObject heldItem;

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Burger"))
    //    {
    //        collision.transform.SetParent(hold, true);
    //        heldItem = collision.gameObject;
    //        heldItem.transform.position = hold.position;
    //        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}


    private void ClipToPan()
    {
        
    }
}
