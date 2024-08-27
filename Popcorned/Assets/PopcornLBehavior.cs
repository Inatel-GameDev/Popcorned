using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornLBehavior : MonoBehaviour
{
    public Vector2 force = new Vector2(0.5f,0f);
    public Vector2 deadzone = new Vector2();

    private Vector3 rotSpeed = new Vector3(0f, 0f, -360f);
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);
        if (transform.position.x < -deadzone.x || transform.position.x > deadzone.x || transform.position.y > deadzone.y || transform.position.y < -deadzone.y)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerBehavior.Instance.parringL)
        {
            rotSpeed.z = -rotSpeed.z;
            rb.AddForce(-3*force, ForceMode2D.Impulse);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}