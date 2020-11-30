using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookAt : MonoBehaviour
{
    public GameObject lookAtTarget;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = lookAtTarget.transform.position;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
