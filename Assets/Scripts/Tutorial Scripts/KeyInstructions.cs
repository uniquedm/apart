using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInstructions : MonoBehaviour
{
    public KeyCode[] keyCodes;

    // Update is called once per frame
    void Update()
    {
        foreach (var key in keyCodes)
        {
            if (Input.GetKeyDown(key) || Input.GetKey(key))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
