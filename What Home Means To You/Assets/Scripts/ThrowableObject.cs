using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float throwSpeed;
    public float spinSpeed;
    public bool grabbable = true;
    
    public void Throw(Vector2 startPos, Vector2 direction)
    {
        var rb = GetComponent<Rigidbody2D>();
        transform.parent = null;
        rb.simulated = true;
        rb.velocity = (direction * throwSpeed);
        grabbable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
