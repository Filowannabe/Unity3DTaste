using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Quaternion originLocalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        updateSway();
    }

    private void updateSway()
    {
        float t_xLookInput = Input.GetAxis("Mouse X");
        float t_yLookInput = Input.GetAxis("Mouse Y");

        Quaternion t_xAngleAdjutment = Quaternion.AngleAxis(-t_xLookInput * 1.45f, Vector3.up);
        Quaternion t_yAngleAdjutment = Quaternion.AngleAxis(t_xLookInput * 1.45f, Vector3.right);
        Quaternion t_targetRotation = t_xAngleAdjutment * t_yAngleAdjutment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, t_targetRotation, Time.deltaTime * 10f);
    }
}
