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
    bool _onGround;
    CapsuleCollider2D myCollider;
    bool OnGround { get { return _onGround; }
        set
        {
            if (_onGround != value)
            {
                _onGround = value;
                AnimatorsSetBool("jump", !value);
            }
        }
    }
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        rb.velocity = input * walkSpeed + Vector2.up * rb.velocity.y;

        AnimatorsSetFloat("speed", Mathf.Abs(input.x));

        Jump();
        var mouseWorldPosition = MouseWorldPosition();
        UpdateScale(mouseWorldPosition - transform.position);

        if (Input.GetButtonDown("Fire1"))
            WorldGenerator.SetTile(mouseWorldPosition);

        if (Input.GetButtonDown("Fire2"))
            WorldGenerator.DeleteTile(mouseWorldPosition);

        var hit = Physics2D.BoxCast(transform.position + Vector3.up * myCollider.offset.y,
            new Vector2(myCollider.size.x * (1 - Constants.raycastEpsilon), Constants.raycastEpsilon),
            0,
            Vector2.down,
            myCollider.offset.y);
        OnGround = (hit.collider != null);
    }
    void UpdateScale(Vector3 mouseDirection)
    {
        if (mouseDirection.x != 0 && !animators[0].GetBool("wall"))
            transform.localScale = new Vector3(Mathf.Sign(mouseDirection.x), 1, 1);
    }
    Vector3 MouseWorldPosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
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
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && OnGround)
        { 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
