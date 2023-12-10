using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    //  The object that is modified by pressing this button
    public GameObject LinkedObject;
    //  Used to animate the button when pressed and unpressed
    private Animator _animator;


    //  Stops the button from calling the linked object's function if it was already held down
    private int _numberOfObjectsOnButton = 0;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasCustomTag("ButtonInteract"))
        {
            //  Only activate for the first object to press the button
            if (_numberOfObjectsOnButton <= 0)
            {
                //  Call the method named "ButtonActivate" from the linked object
                if (LinkedObject != null)
                    LinkedObject.SendMessage("ButtonActivate");

                //  Press animation
                _animator.SetTrigger("ButtonPress");
            }

            _numberOfObjectsOnButton += 1;

            Debug.Log(_numberOfObjectsOnButton);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.HasCustomTag("ButtonInteract"))
        {
            _numberOfObjectsOnButton -= 1;

            //  Only deactivate when the last object on the button leaves
            if (_numberOfObjectsOnButton <= 0)
            {
                //  Call the method named "ButtonDeactivate" from the linked object
                if (LinkedObject != null)
                    LinkedObject.SendMessage("ButtonDeactivate");

                //  Unpress animation
                _animator.SetTrigger("ButtonUnpress");
            }

            Debug.Log(_numberOfObjectsOnButton);
        }
    }
}
