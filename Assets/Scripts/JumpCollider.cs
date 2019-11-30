using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{
    JumpCharacter jumpCharacter;
    float groundedResetTimer;
    public bool handleGrounded = false;
    public bool rightHang = false;
    public bool leftHang = false;
    void Start()
    {
        jumpCharacter = GetComponentInParent<JumpCharacter>();
    }
    void Update()
    {
        if (handleGrounded && Time.time - groundedResetTimer > 0.1f)
        {
            jumpCharacter.grounded = false;
        }
        if (rightHang && Time.time - groundedResetTimer > 0.1f)
        {
            jumpCharacter.rightHang = false;
        }
        if (leftHang && Time.time - groundedResetTimer > 0.1f)
        {
            jumpCharacter.leftHang = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag != "Wall")
        {
            return;
        }
        groundedResetTimer = Time.time;
        if (handleGrounded)
        {
            if (!jumpCharacter.grounded)
            {
                jumpCharacter.Land(transform.position);
            }
            jumpCharacter.grounded = true;
        }
        if (rightHang)
        {
            if (!jumpCharacter.rightHang && Controls.Right)
            {
                jumpCharacter.Land(transform.position);
            }
            jumpCharacter.rightHang = true;
        }
        if (leftHang)
        {
            if (!jumpCharacter.leftHang && Controls.Left)
            {
                jumpCharacter.Land(transform.position);
            }
            jumpCharacter.leftHang = true;
        }
        jumpCharacter.canDouble = true;
    }
}