using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : MonoBehaviour
{
    public float cuttingTime;
    public List<GameObject> cuttableIngredience;
    public GameObject PlacedIngredience;
    public bool isPlaced;
    public Transform PSpot;
    private GameObject CVersion;

    private void Update()
    {
        
        if (isPlaced && cuttableIngredience.Contains(PlacedIngredience)) {
            if (PlacedIngredience.TryGetComponent<ingred>(out ingred Ingred))
            {
                    CVersion = Ingred.CuttVersion;
                    PlacedIngredience.transform.SetParent(null);
                    Destroy(PlacedIngredience);
                    PlacedIngredience = CVersion;
                    PlacedIngredience.transform.SetParent(PSpot, true);
                    PlacedIngredience.transform.localPosition = Vector3.zero;
            }
        }
    }
}
