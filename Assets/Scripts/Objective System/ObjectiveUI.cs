using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectiveUI: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public float duration=5;
    public GameObject memoButton;
    Animator anim;
    float elapsed;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed>duration)
        {
            anim.Play("Close Memo");
            Invoke("DisableMemo",0.5f);
        }
    }

    void DisableMemo()
    {
        gameObject.SetActive(false);
        memoButton.SetActive(true);
        elapsed = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        elapsed = 0;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        elapsed = 0;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        elapsed = 0;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        elapsed = 0;
    }
    public void OnPointerClick (PointerEventData eventData)
    {
        elapsed = 0;
    }
}
