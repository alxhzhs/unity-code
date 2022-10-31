using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_move : MonoBehaviour
{
    float moveSpeed = 0.0f;
    float mouseX = 0.0f;
    float JumpPower = 7.0f;

    [SerializeField] float normalSpeed = 10.0f, runSpeed = 13.0f;

    
    Rigidbody rgBody;

    Animator anim;

    bool canjump = true;

    private void FixedUpdate()
    {
        if (GameManagerLogic.game_stop == false)
        {
            move();
        }
    }
    private void Update()
    {
        if (GameManagerLogic.game_stop == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
                canjump = false;
            }
        }
    }
    void move()
    {
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        float amount = moveSpeed * Time.deltaTime * vert;
        float amounthorz = moveSpeed * Time.deltaTime * horz;

        rgBody.MovePosition(rgBody.position + transform.forward * amount);
        rgBody.MovePosition(rgBody.position + transform.right * amounthorz);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("move", 10f);
            }
            else
            {
                anim.SetFloat("move", normalSpeed);
            }
        }
        else
        {
            anim.SetFloat("move", 0f);
        }
        mouseX = Input.GetAxis("Mouse X") * GameManagerLogic.DPI;
        rgBody.MoveRotation(rgBody.rotation * Quaternion.Euler(0 ,mouseX ,0));
        //transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
    void jump()
    {
        if (canjump)
        {
            rgBody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
        else
        {
            return;
        }
    }

    private void OnCollisionStay(Collision col)
    {
            canjump = true;
    }
    private void OnCollisionExit(Collision col)
    {
        canjump = false;
    }

    private void Start()
    {
        rgBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }


}
