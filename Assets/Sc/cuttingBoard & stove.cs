using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : MonoBehaviour
{
    private float StartcuttingTime = 5f;
    private float StartburnTime = 10f;
    public float cuttingTime;
    public float burnTime;
    //reference na objeky
    public GameObject PlacedIngredienceA;
    public GameObject PlacedIngredienceB;
    private GameObject ItemToDestroy;
    public Transform PSpot;

    public bool isPlaced;
    public bool isStove;
    public bool isCuttingBoard;


    public void Start()
    {
        isPlaced = false;
        cuttingTime = StartcuttingTime;
        burnTime = StartburnTime;
    }
    private void Update()
    {
        if (!isPlaced)
        {
            cuttingTime = StartcuttingTime;
            burnTime = StartburnTime;
        }

        if (isPlaced) 
        {
            cuttingTime -= Time.deltaTime;
            burnTime -= Time.deltaTime;
            if (isCuttingBoard)
            {
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingred) && Ingred.isCuttable)
                {
                    cutting(Ingred);
                }
            }

            else if (isStove) {
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingredience) && Ingredience.isCookable || Ingredience.isBurnable)
                {
                    cook(Ingredience);
                }
            }
        }
        
    }


    private void cutting(ingred I)
    {
        if (cuttingTime <= 0)
        {
            if (I.isBun)
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                PlacedIngredienceB = Instantiate(I.CookingVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
                PlacedIngredienceB.transform.localPosition = Vector3.zero;
                cuttingTime = 5;
            }
            else
            {
                ItemToDestroy = PlacedIngredienceA;
                PlacedIngredienceA = Instantiate(I.CuttVersion, PSpot);
                Destroy(ItemToDestroy);
                PlacedIngredienceA.transform.localPosition = Vector3.zero;
                cuttingTime = 5;
            }
            }
    }
    private void cook (ingred I)
    {
        if (cuttingTime <= 0 && I.isCookable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
        }

        else if (burnTime <= 0 && I.isBurnable)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.BurdenVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;

        }
    }
}
