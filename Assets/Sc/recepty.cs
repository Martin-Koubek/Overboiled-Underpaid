using System.Collections.Generic;
using UnityEngine;

public abstract class recepty : MonoBehaviour
{ 
    [SerializeField]
    private List<GameObject> validStart = new List<GameObject>();
    [SerializeField]
    private List<GameObject> validIngred = new List<GameObject>();
    [SerializeField]
    private List<GameObject> TempValidIngred = new List<GameObject>();
    [SerializeField]
    private List<GameObject> validEnd = new List<GameObject>();

    public List<GameObject> Order = new List<GameObject>();
    private int MaxOrders = 3;
    private int MaxIngred = 8;
    private int maxPlate;
    public void Start()
    {
        newOrder();
    }
    public void newOrder()
    {
        TempValidIngred = validIngred;
        maxPlate = Random.Range(2, MaxIngred);
        for (int i = 0; i <= maxPlate; i++)
        {
            if (i == 0)
            {
                Order.Add(validStart[0]);
            }
            else if (i == maxPlate)
            {
                Order.Add(validEnd[0]);
            }
            else
            {
                int ingred = Random.Range(0, TempValidIngred.Count - 1);
                if (Order.Contains(TempValidIngred[ingred]))
                {
                    TempValidIngred.Remove(TempValidIngred[ingred]);
                    ingred = Random.Range(0, TempValidIngred.Count - 1);
                }
                else { Order.Add(TempValidIngred[ingred]); }

            }

        }
    }
}