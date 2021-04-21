using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChainEnable : MonoBehaviour
{
    public Transform Chain;

    public void ChainEnableEvent(int _event)
    {
        switch(_event)
        {
            case 0:
                Chain.gameObject.SetActive(false);
                break;
            case 1:
                Chain.gameObject.SetActive(true);
                break;
        }
    }
}
