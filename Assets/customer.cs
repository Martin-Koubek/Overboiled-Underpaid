using NUnit.Framework;
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
    void Update()
    {
        if (!haveOrder)
        {
            newOrder();
            MyOrder = Order;
            haveOrder = true;
        }
        else if (haveOrder)
        {
            for (int i = 0; i < MyOrder.Count - 1; i++)
            {
                for(int j = 0; j<showSpot.Count-1; j++)
                {

                }
            }
        }
    }
}
