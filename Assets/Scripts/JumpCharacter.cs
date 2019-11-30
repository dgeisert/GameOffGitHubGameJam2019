using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharacter : MonoBehaviour
{
    float jumpStarted;
    public bool grounded = true;
    public bool canDouble = false;
    bool jumpHeld = false;
    public float jumpForce = 3;
    public float glideForce = 5;
    public float moveSpeed = 1;
    public float downSpeed = 3;
    public bool rightHang = false;
    public bool leftHang = false;
    public GameObject jumpEffect;
    public GameObject landEffect;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.Instance.active)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        //Jumping
        if (Controls.Jump && grounded && !jumpHeld)
        {
            grounded = false;
            Jump();
        }
        else if (Controls.Jump && canDouble && !jumpHeld)
        {
            canDouble = false;
            Jump();
        }
        else if (Controls.Jump && jumpHeld)
        {
            float f = Mathf.Min(10, 50 * (0.25f * (Time.time - jumpStarted)));
            rb.AddForce(-Vector3.up * Time.deltaTime * glideForce * f, ForceMode.VelocityChange);
        }
        else if (!Controls.Jump)
        {
            jumpHeld = false;
            if (!grounded)
            {
                rb.AddForce(-Vector3.up * Time.deltaTime * glideForce * 10, ForceMode.VelocityChange);
            }
        }

        //Holding down to drop fast
        if (Controls.Down)
        {
            rb.velocity = new Vector3(rb.velocity.x, -downSpeed, 0);
        }

        //Left and Right movement
        if (Controls.Right && Controls.Left)
        {
            //rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else if (Controls.Right)
        {
            rb.velocity = new Vector3(Mathf.Min(moveSpeed, rb.velocity.x + moveSpeed / (grounded ? 10 : 20)), rb.velocity.y, 0);
        }
        else if (Controls.Left)
        {
            rb.velocity = new Vector3(Mathf.Max(-moveSpeed, rb.velocity.x - moveSpeed / (grounded ? 10 : 20)), rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, 0);
        }

        //grab ledges
        if (rb.velocity.y < 0 && jumpStarted > 0.25f)
        {
            if (rightHang && Controls.Right)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
            if (leftHang && Controls.Left)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
        }

        Game.Score = Mathf.Max(Game.Score, transform.position.y * 100);
    }

    void Jump()
    {
        jumpEffect.SetActive(false);
        jumpEffect.SetActive(true);
        if (rightHang && !leftHang)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce / 2, 0);
            rb.AddForce(-Vector3.right * jumpForce * 2, ForceMode.VelocityChange);
        }
        if (!rightHang && leftHang)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce / 2, 0);
            rb.AddForce(Vector3.right * jumpForce * 2, ForceMode.VelocityChange);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }
        jumpStarted = Time.time;
        jumpHeld = true;
    }
    public void Land(Vector2 pos)
    {
        landEffect.transform.position = pos;
        landEffect.SetActive(false);
        landEffect.SetActive(true);
    }
}