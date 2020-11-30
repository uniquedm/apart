using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class AlternateVoltaTrigger : MonoBehaviour
{
    public PathFollower pathFollower;
    public FollowInteraction followInteraction;
    public BoxCollider trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!pathFollower.enabled)
            {
                pathFollower.enabled = true;
                followInteraction.enabled = false;
                trigger.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!pathFollower.enabled)
            {
                pathFollower.enabled = true;
                followInteraction.enabled = false;
                trigger.enabled = false;
            }
        }
    }
}
