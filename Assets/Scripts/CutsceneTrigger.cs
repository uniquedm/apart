using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    bool triggered=false;
    public PlayableDirector cutscene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            cutscene.Play();
        }
    }
}
