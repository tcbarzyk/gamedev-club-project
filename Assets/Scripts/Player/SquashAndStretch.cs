using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField]float velScale;
    [SerializeField]float accScale;

    [SerializeField] Vector2 velocity;
    [SerializeField] Vector2 acceleration;

    Rigidbody2D rb;

    MaterialPropertyBlock block;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        block = new MaterialPropertyBlock();
    }

    private void FixedUpdate()
    {
        //squash and stretch?
    }
}
