using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Objective
{
    public string description;
    public string objectiveName;
}

public class ObjectiveSystem : MonoBehaviour
{
    public static int objectiveNo = 0;
    public GameObject objectiveTemplate;
    
    public List<Objective> objectives;
    List<GameObject> objectivesCreated;

    public GameObject objectivePanel;
    public GameObject memoButton;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        objectiveNo = 0;
        objectivesCreated = new List<GameObject>();
    }

    void Awake()
    {
        objectivesCreated = new List<GameObject>();
        ActivePanel();
        for (int i = 0; i < objectives.Count; i++)
        {
            GameObject objective = Instantiate(objectiveTemplate, gameObject.transform);
            objective.transform.SetSiblingIndex(objectiveNo);
            objective.name = objectives[i].objectiveName + "#" + objectiveNo;
            objective.GetComponentInChildren<TextMeshProUGUI>().text = objectives[i].description;
            objective.SetActive(true);
            objectivesCreated.Add(objective);
            objectiveNo++;
        }
    }

    public int addObjective(Objective newObjective)
    {
        ActivePanel();
        GameObject objective = Instantiate(objectiveTemplate, gameObject.transform);
        objective.transform.SetAsLastSibling();
        objective.name = newObjective.objectiveName + "#" + objectiveNo;
        objective.GetComponentInChildren<TextMeshProUGUI>().text = newObjective.description;
        objective.SetActive(true);
        objectivesCreated.Add(objective);
        return objectiveNo++;
    }

    public void removeObjective(int objectiveID)
    {
        ActivePanel();
        //objectivesCreated[objectiveID].GetComponent<Animator>().Play("ObjectiveRemove");
        Destroy(objectivesCreated[objectiveID]);
        //Will be done later :)
        //StartCoroutine(DeleteObjective(objectiveID));
    }
    
    /*IEnumerator DeleteObjective(int objectiveID)
    {
        yield return new WaitForSeconds(1);
        objectivesCreated[objectiveID].GetComponent<Animator>().Play("ObjectiveRemove");
        yield return new WaitForSeconds(1);
        Destroy(objectivesCreated[objectiveID]);
        yield break;
    }*/

    void ActivePanel()
    {
        objectivePanel.SetActive(true);
        memoButton.SetActive(false);
    }
}
