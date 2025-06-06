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
    public List<Material> mat = new List<Material>();
    public Material gone;
    float w;
    public GameObject CustLook;
    public bool waitOrder;


    void Start()
    {
        waitOrder = false;
        CustLook.GetComponent<Renderer>().material = mat[Random.Range(0, mat.Count)];
        //w = Random.Range(10, 25) * Time.deltaTime;
        base.Start();
        showOrder();
    }
    private void Update()
    {
        if (waitOrder)
        {
            CustLook.GetComponent<Renderer>().material = gone;
            orderBg.gameObject.SetActive(false);

            if (w <= 0)
            {
                orderBg.gameObject.SetActive(true);
                CustLook.GetComponent<Renderer>().material = mat[Random.Range(0, mat.Count)];
                base.Start();
                showOrder();
                orderBg.color = Color.white;
                waitOrder = false;
            }
            else { w -= Time.deltaTime; }
        }
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
        w = Random.Range(240, 480) * Time.deltaTime;
        waitOrder = true;
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

