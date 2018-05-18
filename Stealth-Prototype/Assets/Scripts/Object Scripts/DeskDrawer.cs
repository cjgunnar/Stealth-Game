using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDrawer : MonoBehaviour {
    //script to control drawer animation states

    private bool open;

    private Animator animator;

	// Use this for initialization
	void Start () {
        open = false;

        animator = GetComponent<Animator>();
	}
	
    void OnMouseDown()
    {
        if (open)
        {
            animator.SetBool("isOpen", false);
            open = false;
        }
        else
        {
            animator.SetBool("isOpen", true);
            open = true;
        }
    }

}
