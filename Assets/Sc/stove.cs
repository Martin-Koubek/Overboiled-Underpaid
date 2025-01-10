using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stove : MonoBehaviour
{
    public float cookTime;
    public float burnTime;

    public GameObject PlacedIngredience;
    private GameObject ItemToDestroy;
    public Transform PlaceSpot;
    public bool isPlaced;

    private void Update()
    {
        if (isPlaced)
        {
            if(TryGetComponent<ingred>(out ingred Ingred) && Ingred.isCookable)
            {
                cook(Ingred);
            }
        }
    }

    private void cook(ingred I) {


        ItemToDestroy = PlacedIngredience;
        PlacedIngredience = Instantiate(I.CookingVersion, PlaceSpot);
        Destroy(ItemToDestroy);
        PlacedIngredience.transform.localPosition = Vector3.zero;
        
    }
}
