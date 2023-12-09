using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //  The object that is modified by pressing this button
    public GameObject LinkedObject;
    //  Used to animate the button when pressed and unpressed
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //  Call the method named "ButtonActivate" from the linked object
            LinkedObject.SendMessage("ButtonActivate");

            //  Press animation
            _animator.SetTrigger("ButtonPress");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //  Call the method named "ButtonDeactivate" from the linked object
            LinkedObject.SendMessage("ButtonDeactivate");

            //  Unpress animation
            _animator.SetTrigger("ButtonUnpress");
        }
    }
}
