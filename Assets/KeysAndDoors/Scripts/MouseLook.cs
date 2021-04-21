using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 100f;
    private float pitchRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * this.mouseSensitivity * Time.deltaTime;
        //Debug.Log($"Mouse: {mouseX} , {mouseY}");

        // Camera Pitch
        this.pitchRotation -= mouseY;
        this.pitchRotation = Mathf.Clamp(this.pitchRotation, -90, 90);
        this.transform.localRotation = Quaternion.Euler(this.pitchRotation, 0f, 0f);

        // Body yaw
        this.playerBody.Rotate(Vector3.up * mouseX);
    }
}
