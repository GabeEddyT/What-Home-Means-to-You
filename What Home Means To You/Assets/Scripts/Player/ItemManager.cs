using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform Socket;
    ControlButtons buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponent<ControlButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }

    void CheckPickup()
    {
        if (Input.GetKeyDown(buttons.pickupThrow))
        {
            try
            {// Check if holding an object
                Socket.GetChild(0);
                GetComponent<Animator>().Play("Throw");
            }
            catch
            {// Not holding
                var hit = Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Throwable"));
                if (hit && hit.GetComponent<ThrowableObject>().grabbable)
                {
                    hit.transform.parent = Socket;
                    hit.transform.position = Socket.transform.position;
                    hit.GetComponent<Rigidbody2D>().simulated = false;
                    hit.GetComponent<ThrowableObject>().grabbable = false;
                }
            }
        }
    }

    /// <summary>
    /// Gets called from an animation event.
    /// Throws held object.
    /// </summary>
    void ThrowItem()
    {
        var item = Socket.GetChild(0).GetComponent<ThrowableObject>();
        item.Throw(item.transform.position, -transform.right * 2 + transform.up);
    }
}
