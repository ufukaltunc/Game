using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject codelock;
    public Animator animator;

    private void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButton("Use"))
        {
            codelock.SetActive(true);
            animator.SetBool("In", true);
            animator.SetBool("Out", false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("In", false);
        animator.SetBool("Out", true);
    }
}
