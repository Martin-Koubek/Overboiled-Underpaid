using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseLook : MonoBehaviour
{
    public float sens = 100f;

    public Slider sensSlider;

    public Transform player;

    float xrotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sensSlider.value = sens;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -50f, 50f);

        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    public void sensSet()
    {
        sens = sensSlider.value;
    }
}
