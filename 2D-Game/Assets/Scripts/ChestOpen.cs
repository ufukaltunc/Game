using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("In", true);
        animator.SetBool("Out", false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        animator.SetBool("In", true);
        animator.SetBool("Out", false);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("In", false);
        animator.SetBool("Out", true);
    }
}
