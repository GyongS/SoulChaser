using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTargetIndicator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_TargetSprite;

    [SerializeField]
    private RectTransform m_TargetOBJ;

    [SerializeField]
    private GameObject[] m_TargetOBJChild;

    private Image m_TargetIconImg;

    private RectTransform m_TargetIcon;

    [SerializeField]
    RectTransform m_TargetFxIcon;

    [SerializeField]
    private Canvas m_Canvas;

    private bool b_OffScreen;

    [SerializeField]
    private float m_edgeBuffer;

    private void Awake()
    {
        m_TargetIcon = new GameObject().AddComponent<RectTransform>();
        InitTargetIcon();
    }

    private void Update()
    {
        UpdateTargetIconPos();
    }

    private void InitTargetIcon()
    {
        m_TargetIcon.transform.SetParent(m_Canvas.transform);
        m_TargetOBJ.transform.SetParent(m_TargetIcon);
        m_TargetFxIcon.transform.SetParent(m_TargetIcon);
    }

    private void UpdateTargetIconPos()
    {
        Vector3 v_TargetPos = transform.position;

        v_TargetPos = Camera.main.WorldToViewportPoint(v_TargetPos);

        if (v_TargetPos.x > 1) // 오른쪽
        {
            b_OffScreen = true;
            m_TargetOBJChild[0].gameObject.SetActive(false);
            m_TargetOBJChild[1].gameObject.SetActive(true);
            m_TargetIcon.gameObject.SetActive(true);
            m_TargetOBJ.gameObject.SetActive(true);
            m_TargetOBJ.gameObject.GetComponent<Image>().sprite = m_TargetSprite[1];
        }
        else if (v_TargetPos.x < 0) // 왼쪽
        {
            b_OffScreen = true;
            m_TargetOBJChild[1].gameObject.SetActive(false);
            m_TargetOBJChild[0].gameObject.SetActive(true);
            m_TargetIcon.gameObject.SetActive(true);
            m_TargetOBJ.gameObject.SetActive(true);
            m_TargetOBJ.gameObject.GetComponent<Image>().sprite = m_TargetSprite[0];
        }
        else
        {
            b_OffScreen = false;
            m_TargetIcon.gameObject.SetActive(false);
            m_TargetOBJ.gameObject.SetActive(false);
        }

        if (v_TargetPos.z < 0.5f)
        {
            v_TargetPos.x = 1.0f + v_TargetPos.x;
            v_TargetPos.y = 1.0f - v_TargetPos.y;
            v_TargetPos.z = 0;
            v_TargetPos = Vector3MaxMize(v_TargetPos);           
        }

        v_TargetPos = Camera.main.ViewportToScreenPoint(v_TargetPos);
        v_TargetPos.x = Mathf.Clamp(v_TargetPos.x, m_edgeBuffer, Screen.width - m_edgeBuffer);
        v_TargetPos.y = Mathf.Clamp(v_TargetPos.y, m_edgeBuffer, Screen.height - m_edgeBuffer);

        m_TargetIcon.position = v_TargetPos;
        m_TargetOBJ.transform.position = v_TargetPos;
        m_TargetFxIcon.position = v_TargetPos;



        if (b_OffScreen)
        {
            var TargetLocalPos = Camera.main.transform.InverseTransformPoint(transform.position);
            var TargetAngle = -Mathf.Atan2(TargetLocalPos.x, TargetLocalPos.y) * Mathf.Rad2Deg - 90;
            m_TargetIcon.transform.eulerAngles = new Vector3(0, 0, TargetAngle);
        }
        else
        {
            m_TargetIcon.transform.eulerAngles = new Vector3(0, 0, 0);
                
        }
    }

    private Vector3 Vector3MaxMize(Vector3 v3)
    {
        Vector3 v_returnVector = v3;
        float max = 0;
        max = v3.x > max ? v3.x : max;
        max = v3.y > max ? v3.y : max;
        max = v3.z > max ? v3.z : max;
        v_returnVector /= max;
        return v_returnVector;
    }
}
