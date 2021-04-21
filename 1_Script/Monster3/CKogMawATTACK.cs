using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CKogMawATTACK : CKogMawState
{
    [SerializeField] private GameObject[] pBullet;
    [SerializeField] private GameObject pArea;
    [SerializeField] private GameObject pSplash;
    private GameObject[] _pInstanceBullet = new GameObject[3];
   

    private Rigidbody pRigidBody;
    private IEnumerator Fire;
    bool bIsBulletActive = false;
    private float fTime = 0.0f;
    private Collider _MAttackCol;
    IEnumerator Monster_Bullet()
    {
        GameObject[] _pInstanceBullet = new GameObject[3];
        GameObject _DropBullet;
        yield return new WaitForSeconds(0.4f);
        _pInstanceBullet[0] = Instantiate(pBullet[0], transform.position + new Vector3(0f, 2f, 0.5f), transform.rotation);
        _pInstanceBullet[0].GetComponent<Rigidbody>().velocity = transform.up * 15;

        yield return new WaitForSeconds(1.0f);
        _pInstanceBullet[0].SetActive(false);

        if (!_pInstanceBullet[0].activeSelf)
        {
            _DropBullet = Instantiate(pBullet[1], _Manager.GetPlayer.transform.position + new Vector3(0,30,0), Quaternion.Euler(new Vector3(0,0,180)));
            _DropBullet.GetComponent<Rigidbody>().velocity = -transform.up * 30;


            _pInstanceBullet[1] = Instantiate(pArea, _Manager.GetPlayer.transform.position, _Manager.GetPlayer.transform.rotation);
            yield return new WaitForSeconds(1.0f);
            _pInstanceBullet[1].SetActive(false);
            yield return new WaitForSeconds(0.1f);
            _pInstanceBullet[2] = Instantiate(pSplash, _pInstanceBullet[1].transform.position + new Vector3(0, -1, 0), _pInstanceBullet[1].transform.rotation);
            _MAttackCol = _pInstanceBullet[2].GetComponent<SphereCollider>();
            _MAttackCol.enabled = true;
            yield return new WaitForSeconds(0.1f);
            _MAttackCol.enabled = false;
            yield return new WaitForSeconds(2f);
            _pInstanceBullet[2].SetActive(false);

            bIsBulletActive = true;
        }
      
    }

    public override void BeginState()
    {
        base.BeginState();
        _Manager.Rigid.velocity = Vector3.zero;
        Fire = Monster_Bullet();
        StartCoroutine(Fire);
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
       
        _Manager.Player_Checking();
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_Manager.GetDirection), 0.5f);


        AttackEnd();
    }

    public void SetActiveEffect(GameObject pObj, float fMaxTime)
    {
        fTime += Time.deltaTime;
        if (fTime > fMaxTime)
        {
            fTime = 0.0f;
            pObj.SetActive(false);
        }
    }

    public void AttackEnd()
    {
        if ( _Manager.Anim.GetCurrentAnimatorStateInfo(0).IsName("ATTACK") &&
           _Manager.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98)
        {
            _Manager.SetState(EnemyState.DELEY);
            return;
        }
    }

    
}
