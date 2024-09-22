using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam2 : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public float xRot;
    public float yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        //get mosue input

        float mouseX = Input.GetAxisRaw("Rightstick X axis") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Rightstick Y axis") * Time.fixedDeltaTime * sensY;

        yRot += mouseX;
        xRot -= mouseY;
       

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //rotate cam

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

    }


}
