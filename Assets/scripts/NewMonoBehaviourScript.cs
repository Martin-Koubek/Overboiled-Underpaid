using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public Slider slider;

    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        txt.text = "sensitivity:" + slider.value.ToString();
    }
}

