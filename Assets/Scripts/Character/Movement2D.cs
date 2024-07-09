using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] // privat type ������ view �� ���̰� ����
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 8.0f;
    [HideInInspector] // public type ������ view �� ������ �ʰ� ����
    public bool isLongJump = false;

    // �ٴڿ� ����� ���� ����
    [SerializeField]
    private LayerMask groundLayer; // �ٴ� üũ�� ���� �浹 ���̾�
    private BoxCollider2D boxcoliider2D; // ������Ʈ�� �浹 ���� ������Ʈ - ���� boxCollider�� �Ѵٸ� BoxCollider2D �� ����
    [HideInInspector]
    public bool isGrounded; // �ٴ� üũ (�ٴڿ� ������ true)
    private Vector3 footPosition; // ���� ��ġ

    private Rigidbody2D rigid2D;

    private Animator animator;

    public string currentMapName; // transferMap ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����.

    // ������Ʈ�� �����־ 1ȸ ����
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcoliider2D = gameObject.GetComponent<BoxCollider2D>();
    }


    /* 
       Update�� ��ũ��Ʈ�� enabled �����϶�, �� �����Ӹ��� ȣ��˴ϴ�.
       �Ϲ������� ���� ����ϰ� ���Ǵ� �Լ��̸�, ���� ȿ���� ������� ���� ������Ʈ�� �������̳� �ܼ��� Ÿ�̸�, Ű �Է��� ���� �� ���˴ϴ�.
    */

    /* 
       �������� ������� ȣ��Ǵ� Update �� �޸� Fixed Timestep�� ������ ���� ���� ������ �������� ȣ��
       ���� ȿ���� �����(Rigidbody) ������Ʈ�� ������ �� ���˴ϴ�. (Update�� �ұ�Ģ�� ȣ�������� �������� �浹�˻� ���� ����� �ȵ� �� �ִ�)
       ���� ������ �ϱ� �� �����.
    */
    private void FixedUpdate()
    {
        Bounds bounds = boxcoliider2D.bounds; // �÷��̾� ������Ʈ�� Collider2D min, center, max ��ġ ����

        footPosition = new Vector2(bounds.center.x, bounds.min.y); // x ��� y �� �ؿ�

        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer); // �ݶ��̴��� ���� ������ ���ϴ��� Ȯ��.
    }

    private void OnDrawGizmos() // ����� ���� 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        if (x == 0)
        {
            animator.SetBool("isMove", false);
        } else
        {
            animator.SetBool("isMove", true);
        }
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            rigid2D.velocity = Vector2.up * jumpForce;
        }
    }

}
