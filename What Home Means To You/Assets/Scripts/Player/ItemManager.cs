using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform Socket;
    public bool canThrow = true;
    ControlButtons buttons;
    bool lobbedThrow;

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
        if (canThrow && ( Input.GetKeyDown(buttons.throwLobbed) || Input.GetKeyDown(buttons.throwStraight) ))
        {
            if (Input.GetKeyDown(buttons.throwLobbed))
                lobbedThrow = true;
            else
                lobbedThrow = false;

            try
            {// Check if holding an object
                Socket.GetChild(0);
                GetComponent<Animator>().Play("Throw");
            }
            catch
            {// Not holding
                var hit = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Throwable"));
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

        if(lobbedThrow)
            item.Throw(item.transform.position, -transform.right * 2 + transform.up);
        else
            item.Throw(item.transform.position, -transform.right * 2 + (transform.up / 2));
    }
}
