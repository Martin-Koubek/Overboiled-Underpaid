using System.Collections.Generic;
using UnityEngine;

public class customer : recepty
{
    [SerializeField]
    private List<GameObject> showSpot = new();
    [SerializeField]
    private List<GameObject> showCase = new();
    private List<GameObject> MyOrder = new();
    public List<GameObject> delivered = new();
    public bool haveOrder = false;
    int spotToAdd;
    private GameObject toAdd;
    void Update()
    {
        if (!haveOrder)
        {
            newOrder();
            MyOrder = Order;
            spotToAdd = 0;
            haveOrder = true;
        }
        else if (haveOrder)
        {
            for (int i = 0; i < MyOrder.Count - 1; i++)
            {
                foreach (GameObject gm in showCase)
                {
                    if (gm.name == MyOrder[i].name)
                    {
                        Instantiate(gm, showSpot[i].transform);
                    }
                }

            }
        }
    }
}
