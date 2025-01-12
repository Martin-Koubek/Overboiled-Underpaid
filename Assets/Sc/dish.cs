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
    [SerializeField]
    private List<GameObject> ValidIngredienceBowl = new List<GameObject>();
    [SerializeField]
    private List<GameObject> ValidIngrediencePlate = new List<GameObject>() ;
    
    public List<GameObject> PlacedIngredience = new List<GameObject>();
    

    public void Update()
    {
      tillFreez -= Time.deltaTime;  
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ingred>(out ingred I))
        {
            I.TryGetComponent<Rigidbody>(out Rigidbody R);
            if (tillFreez < 0)
            {
                R.constraints = RigidbodyConstraints.FreezeAll;
                R.rotation = Quaternion.identity;
                tillFreez += 3f;
            }

        }
    }
}
