using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class health : MonoBehaviour
{
    public int healthLevel = 3;
    [SerializeField]
    private Light light1;
    [SerializeField]
    private Light light2;
    [SerializeField]
    private Light light3;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light1.intensity = 1;
        light1.intensity = 1;
        light1.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthLevel == 2)
        {
            light1.intensity = 0;
            light2.color = Color.yellow;
            light3.color = Color.yellow;
        }
        if(healthLevel == 1)
        {
            light1.intensity = 0;
            light2.intensity = 0;
            light3.color = Color.red;
        }
        if(healthLevel == 0)
        {
            light1.intensity = 0;
            light2.intensity = 0;
            light3.intensity = 0;
            //TryGetComponent<GameMenu>(out GameMenu g);
            //g.Fail = true;
        }
    }
}
