using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    static PlayerMovement player1, player2;
    // Start is called before the first frame update
    void Start()
    {
        var players = FindObjectsOfType<PlayerMovement>();
        player1 = players[0];
        player2 = players[1];
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player1.GetComponent<Collider2D>(), GetComponent<Collider2D>().bounds.max.y - player1.GetComponent<Collider2D>().bounds.min.y > .2f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player2.GetComponent<Collider2D>(), GetComponent<Collider2D>().bounds.max.y - player2.GetComponent<Collider2D>().bounds.min.y > .2f);
    }
}
