using UnityEditor.Animations;
using UnityEngine;

public class taking : MonoBehaviour
{
    public GameObject hand;
    public GameObject spot;
    public AnimatorController animator;
    public bool holding = false;
    public float ThrowF = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            if (!holding)
            {

            }
            else { }

        }
    }

    void Throw()
    {

    }

    void Grab()
    {

    }

}
