using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class dish : MonoBehaviour
{
    public bool isBowl;
    public bool isPlate;
    public Transform dropSpot;
    public float tillFreez = 3f;
    
    public List<GameObject> PlacedIngredience = new List<GameObject>();
   
    public void Update()
    {
        tillFreez -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.TryGetComponent<ingred>(out ingred I))
        {
            Debug.Log("entered");
            I.TryGetComponent<Rigidbody>(out Rigidbody R);
            if (tillFreez <= 0)
            {
                Debug.Log("frozen");
                R.constraints = RigidbodyConstraints.FreezeAll;
                R.rotation = Quaternion.identity;
                tillFreez += 3f;
            }

        }
    }
}
