using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public bool defaultRight;
    SpriteRenderer sprite;
    Rigidbody2D physics;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (physics.velocity.x != 0)
        {
            if (physics.velocity.x < 0 && defaultRight)
            {
                if (defaultRight)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;
            }
            else
            {
                if (defaultRight)
                    sprite.flipX = true;
                else
                    sprite.flipX = false;
            }
        }
    }
}
