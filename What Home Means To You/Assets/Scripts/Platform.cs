using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    static PlayerMovement player1, player2;
    static ThrowableObject[] throwableObjects;
    // Start is called before the first frame update
    void Start()
    {
        throwableObjects = FindObjectsOfType<ThrowableObject>();
        var players = FindObjectsOfType<PlayerMovement>();
        player1 = players[0];
        player2 = players[1];
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player1.GetComponent<Collider2D>(), GetComponent<Collider2D>().bounds.max.y - player1.GetComponent<Collider2D>().bounds.min.y > .2f || Input.GetKey(player1.GetComponent<ControlButtons>().down));
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player2.GetComponent<Collider2D>(), GetComponent<Collider2D>().bounds.max.y - player2.GetComponent<Collider2D>().bounds.min.y > .2f || Input.GetKey(player2.GetComponent<ControlButtons>().down));
    }

    private void LateUpdate()
    {
        foreach (var item in throwableObjects)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), GetComponent<Collider2D>().bounds.max.y - item.GetComponent<Collider2D>().bounds.min.y > .2f);
        }
    }
}
