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
        else
        {
            showOrder();
        }
    }
    private void showOrder() {
        for (int o = 0; o < MyOrder.Count-1; o++)
        {
            for (int i = 0; i < showCase.Count - 1; i++)
            {
                if (MyOrder[o].name == showCase[i].name)
                {
                    Instantiate(showCase[i], showSpot[o].transform);
                    spotToAdd++;
                }
                else if (spotToAdd >= MyOrder.Count)
                {
                    break;
                }
            }

        }
    }
}
