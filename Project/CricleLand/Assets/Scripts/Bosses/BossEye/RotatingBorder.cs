using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBorder : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(timeTest());
    }
    IEnumerator timeTest()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("FaseTwo", true);
    }
    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, new Vector3(0,0,1), Time.deltaTime*10);
    }
}
