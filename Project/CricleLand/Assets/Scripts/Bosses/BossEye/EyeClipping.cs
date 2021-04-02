using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeClipping : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        firstFaseClipping = true;
        secondFaseClipping = false;
        reloading = false;
        StartCoroutine(clip());
    }

    bool firstFaseClipping, secondFaseClipping;
    public bool reloading;

    IEnumerator clip()
    {
        while (firstFaseClipping)
        {
            animator.SetBool("IdleFirst", true);
            animator.SetBool("IsClipping", false);
            yield return new WaitForSeconds(5);
            animator.SetBool("IsClipping", true);
            animator.SetBool("IdleFirst", false);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("IsClipping", false);
        }

        if (reloading)
        {
            reloading = false;
            animator.SetBool("IsReloading", true);
            yield return new WaitForSeconds(4);
            animator.SetBool("IsReloading", false);
            secondFaseClipping = true;
            StartCoroutine(transform.parent.GetComponent<BossEye>().secondFase());
        }

        while (secondFaseClipping)
        {
            animator.SetBool("IdleSecond", true);
            animator.SetBool("IsClippingSecond", false);
            yield return new WaitForSeconds(5);
            animator.SetBool("IsClippingSecond", true);
            animator.SetBool("IdleSecond", false);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void setSecondFase()
    {
        reloading = true;
        firstFaseClipping = false;
        StartCoroutine(transform.parent.GetComponent<BossEye>().secondFase());
    }
}
