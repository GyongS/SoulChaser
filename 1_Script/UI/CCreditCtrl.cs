using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CCreditCtrl : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image _img;

    [SerializeField]
    private GameObject _Credit;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyOption.ESC))
        {
            _Credit.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _Credit.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _img.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _img.color = new Color(1, 1, 1, 0.3921569f);
    }
}
