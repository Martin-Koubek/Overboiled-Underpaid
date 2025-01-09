using System.Collections.Generic;
using UnityEngine;

public class stove : MonoBehaviour
{
    public float coockTime;
    public float burnTime;
    public GameObject PlacedIngred;
    public Transform PlaceSpot;

    [SerializeField] 
    private List<GameObject> ValidIngredience = new List<GameObject>();
}
