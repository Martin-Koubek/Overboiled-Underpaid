using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customer : recepty
{
    [SerializeField]
    private List<GameObject> showSpot = new();
    [SerializeField]
    private List<GameObject> showCase = new();
    public List<GameObject> delivered = new();
    public Image orderBg;
    private GameObject toAdd;

    void Start()
    {
        base.Start();
        showOrder();
    }
    private void showOrder()
    {
        Debug.Log(Order.Count);
        Debug.Log(base.Order.Count);
            for (int o = 0; o < Order.Count; o++)
        {
            Debug.Log(Order[o].name);

            //    Debug.Log(o + "o");
            //    for (int i = 0; i < showCase.Count; i++)
            //    {
            //        Debug.Log(i + "i");

            //        if (Order[o].name == showCase[i].name)
            //        {
            Debug.Log(Order[o].name);
            Instantiate(showCase.Find(a => a.name == Order[o].name), showSpot[o].transform);
            //            spotToAdd++;
            //        }
            //    }

        }
    }
}
