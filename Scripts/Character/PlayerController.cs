using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement;

    public GameManager gameManager;
    private PlayerHealth playerhealth;
    private bool isDead;

    //====npc====//
    GameObject scanObject;
    Vector3 dirVec;
    Rigidbody2D rigid2D;
    float x;
    float y;

    private void Awake()
    {
        movement = GetComponent<Movement2D>();
        playerhealth = GetComponent<PlayerHealth>();
        rigid2D = GetComponent<Rigidbody2D>();
        scanObject = GetComponent<GameObject>();
    }

    private void Update()
    {
        float x = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");

        movement.Move(x);
        if (x == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180.0f, 0);
        }

        bool Up = gameManager.isAction ? false : Input.GetKeyDown(KeyCode.LeftAlt);
        if (Input.GetKeyDown(KeyCode.LeftAlt) && movement.isGrounded == true && Up == true)
        {
            movement.Jump();
        }

        if (playerhealth.health <= 0 && !isDead)
        {
            movement.Move(0);
            isDead = true;
            gameManager.GameOver();
            Debug.Log("Dead");
        }

        bool xDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");

        //Direction
        if (xDown && x == -1)
            dirVec = Vector3.left;
        else if (xDown && x == 1)
            dirVec = Vector3.right;

        if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            gameManager.Action(scanObject);
    }

    void FixedUpdate()
    {
        Vector3 moveVelocity = new Vector3(x, y, 0) * Time.deltaTime;
        this.transform.position += moveVelocity;

        //Ray
        Debug.DrawRay(rigid2D.position, dirVec * 1f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 1f, LayerMask.GetMask("NPC"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}