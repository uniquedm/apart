using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveRemoveTrigger : MonoBehaviour
{
    public ObjectiveSystem objectiveSystem;
    public ObjectiveAddTrigger initialTrigger;

    private void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            objectiveSystem.removeObjective(initialTrigger.GetID);
            Destroy(gameObject);
        }
    }
}