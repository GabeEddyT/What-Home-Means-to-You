using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float throwSpeed;
    public bool grabbable = true;
    public float damage;
    public float weight;
    ItemManager []players;

    Vector2 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<ItemManager>();
        foreach (var item in players)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
        }
    }

    private void Update()
    {
        applyWeight();
    }

    void applyWeight()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity += Vector2.down * weight * Time.deltaTime;
    }

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
        rb.velocity = initialVelocity = (direction * throwSpeed);
        grabbable = true;
    }

    public void Deflect(Collider2D _deflector)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (_deflector.gameObject == players[0].gameObject)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), players[1].GetComponent<Collider2D>(), false);
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), players[0].GetComponent<Collider2D>(), false);
        }

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _deflector, true);

        initialVelocity.x = -initialVelocity.x;

        initialVelocity = rigidbody.velocity = initialVelocity;

        rigidbody.simulated = true;
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
