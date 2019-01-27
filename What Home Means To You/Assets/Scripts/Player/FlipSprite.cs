using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{

    SpriteRenderer sprite;
    Rigidbody2D physics;
    PlayerMovement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();

        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(physics.velocity.x) != 0)
        //{
        //    if (physics.velocity.x < 0 && defaultRight)
        //    {
        //        if (defaultRight)
        //            transform.rotation = Quaternion.Euler(0, 0, 0);
        //        else
        //            transform.rotation = Quaternion.Euler(0, 180, 0);
        //    }
        //    else
        //    {
        //        if (defaultRight)
        //            transform.rotation = Quaternion.Euler(0, 180, 0);
        //        else
        //            transform.rotation = Quaternion.Euler(0, 0, 0);
        //    }
        //}

        if (!movementScript.canMove)
            return;

        if (Input.GetKey(GetComponent<ControlButtons>().right))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(GetComponent<ControlButtons>().left))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
