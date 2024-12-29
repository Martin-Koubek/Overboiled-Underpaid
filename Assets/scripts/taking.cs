using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taking : MonoBehaviour
{
    //reference na prefab ingrediencí
    public GameObject buns, lettuce, burgerMeat, cheese, steak, carot, tomato, ham;
    
    //reference na držený pøedmìt, mýsto držení, bod poèátku raycastu 
    public GameObject heldItem;
    public Transform RayCastPoint;
    public GameObject spot;
    
    //dosah hráèe
    public float IntRange = 5f;
    //drží hráè nìco
    public bool holding;
    //raycast info
    public RaycastHit hit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = new Ray(RayCastPoint.position, RayCastPoint.forward);
            if (!holding && Physics.Raycast(ray, out hit, IntRange))
            {
                Debug.DrawRay(RayCastPoint.position, RayCastPoint.forward * IntRange, Color.red);
                Rigidbody rb;
                if (hit.collider.gameObject.CompareTag("bun storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(buns, spot.transform.position, Quaternion.identity);
                    take();

                }
                else if (hit.collider.gameObject.CompareTag("lettuce storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(lettuce, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.CompareTag("tomato storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(tomato, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.CompareTag("ham storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(ham, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.CompareTag("cheese storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(cheese, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.CompareTag("burger meat storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(burgerMeat, spot.transform.position, Quaternion.identity);
                    take();
                }
                else if (hit.collider.gameObject.CompareTag("carot storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(carot, spot.transform.position, Quaternion.identity);
                    take();
                    
                }
                else if (hit.collider.gameObject.CompareTag("steak storage"))
                {
                    //hand.SetActive(true);
                    heldItem = Instantiate(steak, spot.transform.position, Quaternion.identity);
                    take();  
                }
                else if(hit.collider.gameObject.CompareTag("ingred"))
                {
                    heldItem = hit.collider.gameObject;
                    take();
                }
            }
        }
        else if (holding && Input.GetKey(KeyCode.Q))
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
        heldItem.transform.position = Vector3.zero;
        heldItem.transform.rotation = Quaternion.identity;
        heldItem.transform.SetParent(spot.transform, false); 
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
    }
}
