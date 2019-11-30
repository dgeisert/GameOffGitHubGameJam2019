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
        if (col.tag == "Player")
        {
            return;
        }
        groundedResetTimer = Time.time;
        if (handleGrounded)
        {
            jumpCharacter.grounded = true;
        }
        if (rightHang)
        {
            jumpCharacter.rightHang = true;
        }
        if (leftHang)
        {
            jumpCharacter.leftHang = true;
        }
        jumpCharacter.canDouble = true;
    }
}