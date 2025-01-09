using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class cuttingBoard : MonoBehaviour
{
    public float cuttingTime;
    public List<GameObject> cuttableIngredience;
    public GameObject PlacedIngredience;
    public Transform PSpot;
    private GameObject CVersion;

    private void Update()
    {
        if (cuttableIngredience.Contains(PlacedIngredience))
        {
            PlacedIngredience.TryGetComponent<ingred>(out ingred Ingred);
            CVersion = Ingred.CuttVersion;
            PlacedIngredience.transform.SetParent(null);
            Destroy(PlacedIngredience);
            PlacedIngredience = CVersion;
            PlacedIngredience.transform.localPosition = PSpot.position;

        }
    }
}
