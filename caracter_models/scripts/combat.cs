using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat : MonoBehaviour
{
    public bool can_attack = true;
    //ammo Logic Pl
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 25f;
    //
    float attack_cooldown = 1.1f;
    IEnumerator sheath_backer;
    Animator animator;
    Animator MyAnimator;

    void Start()
    {
        currentAmmo = maxAmmo;
        MyAnimator = GameObject.Find("muscle_memory_experemet").GetComponent<Animator>();
        sheath_backer = Snap_back();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("fire_right", false);
        animator.SetBool("chill_fase", false);
        if (Input.GetMouseButtonDown(0))
        {
            if (can_attack)
            {

                Katana_attack();
                animator.SetBool("chill_fase", false);
            }

        }
        if (Input.GetMouseButtonDown(1))
        {

            animator.SetBool("fire_left", true);
            animator.SetBool("fire_right", false);

        }
    }

    void Katana_attack()
    {
        cantAttack();
        int randnum = Random.Range(0, 5);
        animator.SetBool("fire_right", true);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack_only_hand"), 0f);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack"), 1f);
        StopCoroutine("Snap_back");
        StartCoroutine("Snap_back");
        StartCoroutine(reseter_attack());
        animator.SetInteger("attack_index", randnum);
        MyAnimator.SetInteger("Attack_int", randnum);
    }
    IEnumerator Snap_back()
    {
        yield return new WaitForSeconds(1.2f);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack"), 0f);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack_only_hand"), 1f);
        yield return new WaitForSeconds(5);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack_only_hand"), 0f);
        animator.SetLayerWeight(animator.GetLayerIndex("char_hand_attack"), 1f);
        yield return new WaitForSeconds(1);
        animator.SetBool("chill_fase", true);
    }
    IEnumerator reseter_attack()
    {
        yield return new WaitForSeconds(attack_cooldown);
        canAttack();
    }
    void cantAttack()
    {
        can_attack = false;
    }
    void canAttack()
    {
        can_attack = true;

    }
}
