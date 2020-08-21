﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_damage : MonoBehaviour
{
    public Animator animator;
    private bool hitting = false;
    public int damage;
    public float hittingCooldown;
    private float timer;
    private GameObject go;
    
    private void Start()
    {
        animator.SetBool("biting", false);
        timer = hittingCooldown;
    }
    void DoDamage()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            go.GetComponent<PlantsHP>().SmanjiHP(damage);
            timer = hittingCooldown;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Plant" && !hitting)
        {
            animator.SetBool("biting", true);
            hitting = true;
            go = other.gameObject;
            InvokeRepeating("DoDamage", 0, .01667f);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Plant")
        {
            hitting = false;
            animator.SetBool("biting", false);
            CancelInvoke();
        }
    }
}