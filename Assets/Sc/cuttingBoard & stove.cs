using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class cuttingBoard : MonoBehaviour
{
    private float StartcuttingTime = 10f;
    private float StartburnTime = 20f;
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
                if (PlacedIngredienceA.TryGetComponent<ingred>(out ingred Ingredience) && Ingredience.isCookable)
                {
                    cook(Ingredience);
                }
            }
        }
        if (burnTime == 0f)
        {
            cuttingTime = StartcuttingTime;
            burnTime = StartburnTime;
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
        if (cuttingTime <= 0)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.CookingVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
        }

        if (burnTime <= 0)
        {
            ItemToDestroy = PlacedIngredienceA;
            PlacedIngredienceA = Instantiate(I.BurdenVersion, PSpot);
            Destroy(ItemToDestroy);
            PlacedIngredienceA.transform.localPosition = Vector3.zero;
        }
    }
}
