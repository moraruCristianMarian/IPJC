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

    //  Amount of time after stepping off the button before it deactivates the linked object
    [SerializeField]
    private float _deactivateDelay = 0.5f;


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
                //  Stop the deactivation coroutine if it was previously queued
                StopAllCoroutines();

                //  Call the method named "ButtonActivate" from the linked object
                if (LinkedObject != null)
                    LinkedObject.SendMessage("ButtonActivate");

                //  Press animation
                _animator.SetTrigger("ButtonPress");
            }

            _numberOfObjectsOnButton += 1;
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
                if (gameObject.activeSelf)
                    StartCoroutine(Deactivate(_deactivateDelay));
            }
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
    
    IEnumerator Deactivate(float delay)
    {
        yield return new WaitForSeconds(delay);

        //  Call the method named "ButtonDeactivate" from the linked object
        if (LinkedObject != null)
            LinkedObject.SendMessage("ButtonDeactivate");

        //  Unpress animation
        _animator.SetTrigger("ButtonUnpress");
    }
}
