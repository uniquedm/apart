using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class FollowInteraction : MonoBehaviour
{
    bool canInteract;
    PathFollower pathFollower;

    public GameObject interactUI;

    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GetComponent<PathFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            pathFollower.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canInteract = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            interactUI.SetActive(false);
        }
    }
}
