using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float walkSpeed = 1;
    public float jumpForce = 1;
    public Animator[] animators;

    Rigidbody2D rb;

    Vector2 input;
    bool onGround;
    void Start()
    {
        onGround = true;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        AnimatorsSetFloat("speed", Mathf.Abs(input.x));

        Jump();
        UpdateScale(GetMouseDirection());
    }

    void FixedUpdate()
    {
        rb.velocity = input * walkSpeed + Vector2.up * rb.velocity.y;
    }
    void UpdateScale(Vector3 mouseDirection)
    {
        if (mouseDirection.x != 0 && !animators[0].GetBool("wall"))
            transform.localScale = new Vector3(Mathf.Sign(mouseDirection.x), 1, 1);
    }
    Vector3 GetMouseDirection()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos - transform.position;
    }
    void AnimatorsSetFloat(string name, float value)
    {
        foreach (var i in animators)
            i.SetFloat(name, value);            
    }
    void AnimatorsSetBool(string name, bool value)
    {
        foreach (var i in animators)
            i.SetBool(name, value);
    }
    void AnimatorsSetTrigger(string name)
    {
        foreach (var i in animators)
            i.SetTrigger(name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal == Vector2.up)
        {
            onGround = true;
            AnimatorsSetBool("jump", false);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            AnimatorsSetBool("jump", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
