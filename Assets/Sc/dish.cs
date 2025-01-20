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
    public bool placed;
    
    public List<GameObject> PlacedIngredience = new List<GameObject>();
   
    public void Update()
    {

    }

    void OnTriggerEnter()
    {
    
    }
}
