using UnityEngine;

public class UiPlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
    }
}
