using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveAddTrigger : MonoBehaviour
{
    public Objective newObjective;
    public ObjectiveSystem objectiveSystem;
    int objectiveID;

    int count = 0;

    public int GetID
    {
        get
        {
            return objectiveID;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (count == 0)
        {
            if (collision.CompareTag("Player"))
            {
                count++;
                objectiveID = objectiveSystem.addObjective(newObjective);
                gameObject.SetActive(false);
            }
        }
    }
}
