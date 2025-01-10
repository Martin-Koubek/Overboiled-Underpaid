using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : MonoBehaviour
{
    public float cuttingTime;

    //reference na objeky
    public GameObject PlacedIngredience;
    private GameObject ItemToDestroy;
    public Transform PSpot;

    public bool isPlaced;
    

    private void Update()
    {
        
        if (isPlaced) 
        {
            if(PlacedIngredience.TryGetComponent<ingred>(out ingred Ingred) && Ingred.isCuttable)
            {
                cutting(Ingred);
            }
        }
    }


    private void cutting(ingred I)
    {
        ItemToDestroy = PlacedIngredience;
        PlacedIngredience = Instantiate(I.CuttVersion, PSpot);
        Destroy(ItemToDestroy);
        PlacedIngredience.transform.localPosition = Vector3.zero;
    }
}
