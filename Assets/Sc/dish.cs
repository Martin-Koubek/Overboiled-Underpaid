using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class dish : MonoBehaviour
{
    public bool isBowl;
    public bool isPlate;
    public Collider Collider;
    public Transform dropSpot;
    public float tillFreez = 10f;
    public bool placed;
    
    public List<GameObject> PlacedIngredience = new List<GameObject>();
   
    public void Update()
    {
        if (placed) tillFreez -= Time.deltaTime;
    }

    void OnTriggerEnter()
    {
        foreach(GameObject gameObject in PlacedIngredience)
        {
            gameObject.TryGetComponent<Rigidbody>(out Rigidbody Rig);
            if (tillFreez <= 0)
            {
                Debug.Log("frozen");
                Rig.constraints = RigidbodyConstraints.FreezeAll;
                tillFreez = 10f;
                placed = false;
            }
        }
    }
}
