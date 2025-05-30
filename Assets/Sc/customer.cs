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
    public List<GameObject> shown = new();
    public Image orderBg;
    public AudioSource zvonek;

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
            Debug.Log(Order[o].name);
            shown.Add(Instantiate(showCase.Find(a => a.name == Order[o].name), showSpot[o].transform));
        }
    }
    public void newOrd()
    {
        base.Start();
        showOrder();
    }
    public void removeViz()
    {
        foreach (GameObject s in shown)
        {
            Destroy(s);
        }
        Order.Clear();
    }
}

