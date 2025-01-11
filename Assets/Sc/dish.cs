using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class dish : MonoBehaviour
{

    public struct ingred_on_plate
    {
        public GameObject on;
        public GameObject placedOn;
    }


    public bool isBowl;
    public bool isPlate;
    [SerializeField]
    private List<GameObject> ValidIngredienceBowl = new List<GameObject>();
    [SerializeField]
    private List<GameObject> ValidIngrediencePlate = new List<GameObject>() ;
    
    public List<ingred_on_plate> PlacedIngredience = new List<ingred_on_plate>();

    public void Start()
    {
        if (isBowl)
        {
            foreach(GameObject G in ValidIngredienceBowl)
            {
                G.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject G in ValidIngrediencePlate)
            {
                G.SetActive(false);
            }
        }
    }

    public void Update()
    {
        if (isBowl)
        {
            foreach(ingred_on_plate placedOn in PlacedIngredience)
            {
                //if( == ValidIngredienceBowl)
            }
        }
        else if (isPlate)
        {
            //foreach (GameObject placed in PlacedIngredience)
            //{
            //    foreach (GameObject valid in ValidIngrediencePlate)
            //    {
            //        if (placed == valid)
            //        {
            //            valid.SetActive(true);
            //        }

            //    }
            //}
        }
    }
}
