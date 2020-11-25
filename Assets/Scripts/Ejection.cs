using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejection : MonoBehaviour
{
    public static int phase=-1;
    public Animator[] animations;
    public GameObject[] activate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            phase++;
            if(phase<animations.Length)
            {
                animations[phase].enabled = true;
                activate[phase].SetActive(true);
            }
        }
    }
}
