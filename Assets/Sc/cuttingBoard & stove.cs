using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class cuttingBoard : MonoBehaviour
{
    public float cuttingTime = 5f;
    public float burnTime = 10f;
    //reference na objeky
    public GameObject PlacedIngredience;
    private GameObject ItemToDestroy;
    public Transform PSpot;

    public bool isPlaced;
    

    private void Update()
    {
        if (isPlaced) 
        {
            cuttingTime -= Time.deltaTime;
            burnTime -= Time.deltaTime;
            if(PlacedIngredience.TryGetComponent<ingred>(out ingred Ingred) && Ingred.isCuttable)
            {
                cutting(Ingred);
            }
            else if (PlacedIngredience.TryGetComponent<ingred>(out ingred Ingredience) && Ingredience.isCookable)
            {
                cook(Ingredience);
            }
        }
    }


    private void cutting(ingred I)
    {
        if (cuttingTime <= 0)
        {
            ItemToDestroy = PlacedIngredience;
            PlacedIngredience = Instantiate(I.CuttVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredience.transform.localPosition = Vector3.zero;
            cuttingTime = 5;
        }
    }
    private void cook (ingred I)
    {
        if (cuttingTime <= 0)
        {
            ItemToDestroy = PlacedIngredience;
            PlacedIngredience = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredience.transform.localPosition = Vector3.zero;
        }
        if (burnTime <= 0)
        {
            ItemToDestroy = PlacedIngredience;
            PlacedIngredience = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredience.transform.localPosition = Vector3.zero;
            burnTime = 10;
            cuttingTime = 5;
        }
    }
}
