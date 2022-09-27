using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    //[SerializeField] float weaponLength=100f;
    [SerializeField] float weaponDamage=25f;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 65f, Color.blue);
        if (canDealDamage)
        {
            RaycastHit hit;

            int layerMask = 1 << 6;
            if (Physics.Raycast(transform.position, transform.up*65f , out hit,/* 65f,*/ layerMask))
            {
                if (hit.transform.TryGetComponent(out enemyScript enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemy.TakeDamage(weaponDamage);
                    //enemy.HitVFX(hit.point);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    //}
}
