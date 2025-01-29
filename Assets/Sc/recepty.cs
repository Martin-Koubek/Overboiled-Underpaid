using System.Collections.Generic;
using UnityEngine;

public class recepty : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    [SerializeField]
    private List<GameObject> validStart = new List<GameObject>();
    [SerializeField]
    private List<GameObject> validIngred = new List<GameObject>();
    [SerializeField]
    private List<GameObject> TempValidIngred = new List<GameObject>();
    [SerializeField]
    private List<GameObject> validEnd = new List<GameObject>();

    public List<GameObject> Order = new List<GameObject>();
    private int MaxOrders = 4;
    private int MaxIngred = 6;
    private int maxPlate;
    public void Start()
    {
        TempValidIngred = validIngred;
        maxPlate = Random.Range(2, MaxIngred);
        newOrder();
    }
    public void Update()
    {
    }
    private void newOrder()
    {
        for (int i = 0; i <= maxPlate; i++)
        {
            if (i == 0)
            {
                Order.Add(validStart[0]);
            }
            else if (i == maxPlate)
            {
                //int last = Random.Range(0, validEnd.Count);
                Order.Add(validEnd[0]);
                //TempValidIngred = validIngred;

            }
            else
            {
                int ingred = Random.Range(0, TempValidIngred.Count - 1);
                Order.Add(TempValidIngred[ingred]);
                //TempValidIngred.RemoveAt(ingred);

            }

        }
    }
}