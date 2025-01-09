using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class dish : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ValidIngredience = new List<GameObject>();
    [SerializeField]
    private List<GameObject> PlacedIngredience = new List<GameObject>();
}
