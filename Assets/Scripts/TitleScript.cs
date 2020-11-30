using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.enabled = false;
        sfxSource.enabled = false;
    }
}
