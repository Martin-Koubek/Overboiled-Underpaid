using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taking : MonoBehaviour
{
    //reference na prefab ingrediencí
    public GameObject buns, letus, burgerMeat, cheese;
 
    public GameObject hand;
    public Transform player;
    public GameObject spot;
    public float IntRange = 5f;
    public bool holding;
    public float ThrowF = 5f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            Ray ray = new Ray(player.position, player.forward);
            if (!holding && Physics.Raycast(ray, out RaycastHit rayInfo, IntRange))
            {
                if(rayInfo.collider.gameObject.CompareTag("bun storage"))
                {
                    holding = true;
                    hand.SetActive(true);
                    Instantiate(buns, spot.transform.position, Quaternion.identity);
                }
                if (rayInfo.collider.gameObject.CompareTag("letus storage"))
                {
                    holding = true;
                    hand.SetActive(true);
                    Instantiate(letus, spot.transform.position, Quaternion.identity);
                }
                if (rayInfo.collider.gameObject.CompareTag("cheese storage"))
                {
                    holding = true;
                    hand.SetActive(true);
                    Instantiate(cheese, spot.transform.position, Quaternion.identity);
                }
            }

        }
        //na Q bude throw
    }
}
