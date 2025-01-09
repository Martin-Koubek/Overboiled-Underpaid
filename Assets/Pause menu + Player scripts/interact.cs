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
    public GameObject holdSpot;

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
                if (hit.collider.gameObject.TryGetComponent<storage>(out storage s))
                {
                    heldItem = Instantiate(s.FoodRef, holdSpot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.GetComponent<ingred>())
                {
                    heldItem = hit.collider.gameObject;
                    take();
                }
                else if (hit.collider.gameObject.TryGetComponent<dishes>(out dishes d))
                {
                    heldItem = Instantiate(d.dish, holdSpot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.TryGetComponent<Table>(out Table table) && table.isPlaced)
                {
                    heldItem = table.placedItem;
                    table.placedItem.transform.SetParent(null);
                    take();
                    table.isPlaced = false;
                }
                else if (hit.collider.gameObject.TryGetComponent<cuttingBoard>(out cuttingBoard c))
                {
                    heldItem = c.PlacedIngredience;
                    take();
                }
            }
            else if (holding && Physics.Raycast(ray, out hit, HitRange, InteractMask))
            {

                if(hit.collider.gameObject.TryGetComponent<Table>(out Table table))
                {
                    if (!table.isPlaced)
                    {
                        table.placedItem = heldItem;
                        table.isPlaced = true;
                        Place();
                    }
                    return;
                }

                if (heldItem.GetComponent<ingred>())
                {
                    if (hit.collider.TryGetComponent<stove>(out stove stove))
                    {
                        stove.PlacedIngred = heldItem;
                        Place();
                    }
                    if (hit.collider.TryGetComponent<cuttingBoard>(out cuttingBoard c))
                    {
                        c.PlacedIngredience = heldItem;
                        Place();
                    }
                }
            }
        }
        else if (holding && Input.GetKeyDown(KeyCode.Q))
        {
            drop();
        }
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
        heldItem.transform.SetParent(holdSpot.transform, false);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.rotation = Quaternion.identity;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void Place()
    {
        heldItem.transform.SetParent(null);
        heldItem.transform.SetParent(hit.collider.transform, true);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
        holding = false;
    }
}


