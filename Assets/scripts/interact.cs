using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class interact : MonoBehaviour
{    
    //reference na držený pøedmìt, mýsto držení, bod poèátku raycastu 
    public GameObject heldItem;
    public Transform RayCastPoint;
    public GameObject spot;

    public LayerMask PickUpMask;
    public LayerMask InteractMask;
    public LayerMask pan;
    
    //dosah hráèe
    public float HitRange = 5f;
    //drží hráè nìco ?
    public bool holding;
    //raycast info
    public RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(RayCastPoint.position, RayCastPoint.forward);
            if (!holding && Physics.Raycast(ray, out hit, HitRange))
            {
                Debug.DrawRay(RayCastPoint.position, RayCastPoint.forward * HitRange, Color.red);
                //Rigidbody rb;
                if (hit.collider.gameObject.TryGetComponent<storage>(out storage s))
                {
                    heldItem = Instantiate(s.FoodRef, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.GetComponent<ingred>())
                {
                    heldItem = hit.collider.gameObject;
                    take();
                }
                else if (hit.collider.gameObject.TryGetComponent(out dishes d))
                {
                    heldItem = Instantiate(d.dish, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.TryGetComponent<pan>(out pan p))
                {
                    if (!p.isPlaced) {
                        heldItem = hit.collider.gameObject;
                        take();
                    }
                    else
                    {
                        heldItem = p.gameObject;
                        take();
                    }
                }
               


            }



            else if (holding && Physics.Raycast(ray, out hit, HitRange, InteractMask))
            {

                if (heldItem.GetComponent<ingred>())
                {
                    if (hit.collider.TryGetComponent(out pan p))
                    {

                        heldItem.transform.SetParent(null);
                        heldItem.transform.SetParent(hit.collider.transform, true);
                        p.PlacedItem = heldItem;
                        heldItem.GetComponent<Rigidbody>().isKinematic = false;
                        p.isPlaced = true;
                        holding = false;

                    }
                    if (hit.collider.TryGetComponent(out cuttingBoard c))
                    {
                        Transform t = c.PSpot;
                        heldItem.transform.SetParent(null);
                        heldItem.transform.SetParent(hit.collider.transform, true);
                        heldItem.transform.position = t.position;
                        heldItem.GetComponent<Rigidbody>().isKinematic = false;
                        holding = false;

                    }
                }
            }
        }
        else if (holding && Input.GetKeyDown(KeyCode.Q))
        {
            drop();
        }
    }
       
        
    public void StorageTake(RaycastHit hit)
    {
        
    }
    public void drop()
    {
        heldItem.transform.SetParent(null);
        holding = false;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void take()
    {
        holding = true;
        heldItem.transform.position = Vector3.zero;
        heldItem.transform.rotation = Quaternion.identity;
        heldItem.transform.SetParent(spot.transform, false); 
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }
}


