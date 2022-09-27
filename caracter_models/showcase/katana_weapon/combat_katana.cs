using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_katana : MonoBehaviour
{
    bool can_attack = true;
    float attack_cooldown = 1.1f;
    IEnumerator sheath_backer;
    // ------ammoLogic
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime=25f;
    public combat men;
    //
    Animator animator;

    //public Animator MyScript;
    void Start()
    {
        currentAmmo = maxAmmo;
        animator = GetComponent<Animator>();
        sheath_backer = Snap_back();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo <= 0)
        {
            men.can_attack = false;
        }
        animator.SetBool("chill_fase", false);
        //int random = MyScript.GetInteger("attack_index");
        //Debug.Log("katan"+random);
        //Debug.Log(MyScript.Katana_attack.random);
        animator.SetBool("fire_right", false);
        if (Input.GetMouseButtonDown(0))
        {
            if (can_attack && currentAmmo != 0)
            {
                
                Katana_attack();
                animator.SetBool("chill_fase", false);
                //Debug.Log("Right Mouse Button Clicked");
            }

        }
    }
    void Katana_attack()
    {
        
        can_attack = false;
        animator.SetBool("fire_right", true);
        StopCoroutine("Snap_back");
        StartCoroutine("Snap_back");
        StartCoroutine(reseter_attack());
        //animator.SetInteger("Attack_int", MyScript.GetInteger("attack_index"));
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo += 1;
        men.can_attack = true;
    }
    IEnumerator Snap_back()
    {
        //animator.SetBool("chill_fase", false);   
        yield return new WaitForSeconds(7.2f);
        animator.SetBool("chill_fase", true);
    }
    IEnumerator reseter_attack()
    {
        yield return new WaitForSeconds(attack_cooldown);
        can_attack = true;
    }
    public void StartDealDamage()
    {
        this.GetComponentInChildren<DamageDealer>().StartDealDamage();
        currentAmmo--;
        StartCoroutine(Reload());
        Debug.Log(currentAmmo);
    }
    public void EndDealDamage()
    {
        this.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
