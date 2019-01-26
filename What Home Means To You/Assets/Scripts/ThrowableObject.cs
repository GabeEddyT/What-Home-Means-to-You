using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float throwSpeed;
    public float spinSpeed;
    public bool grabbable = true;
    ItemManager []players;
    
    
    public void Throw(Vector2 startPos, Vector2 direction)
    {
        var rb = GetComponent<Rigidbody2D>();
        // Figure out who's holding this, and enable collisions for the opposing player.
        if (transform.parent == players[0].Socket)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), players[1].GetComponent<Collider2D>(), false);
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), players[0].GetComponent<Collider2D>(), false);
        }
        transform.parent = null;
        rb.simulated = true;
        rb.velocity = (direction * throwSpeed);
        grabbable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<ItemManager>();
        foreach (var item in players)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Disable collisions on players if the object lands or hits someone.
        foreach (var item in players)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
        }
    }
}
