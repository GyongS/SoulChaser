using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraShake : MonoBehaviour
{

    private float fShakePower = 0.0f;
    private float fDamping = 0.001f;

    [SerializeField] bool bIsShake = false;
    Vector3 ShakingPosition = Vector3.zero;
    Vector3 RandnomRange = Vector3.zero;
    void Awake()
    {
       
    }
   
    public void OnShaking(float power, float damping, 
                        Vector3 Range , bool button )
    {
        ShakingPosition = transform.position;
        bIsShake = button;
        fShakePower = power;
        fDamping = damping;
        RandnomRange = Range;
    }

    void Update()
    {
            if (fShakePower > 0)
            {
                if (bIsShake)
                    transform.position = RandnomRange
                    * fShakePower + ShakingPosition;

                fShakePower -= fDamping;
            }
            else
            {
                fShakePower = 0.0f;
                bIsShake = false;
                transform.position = transform.parent.position;
            }
    }

   
}
