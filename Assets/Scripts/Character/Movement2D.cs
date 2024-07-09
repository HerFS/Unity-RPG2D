using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] // privat type 변수를 view 에 보이게 설정
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 8.0f;
    [HideInInspector] // public type 변수를 view 에 보이지 않게 설정
    public bool isLongJump = false;

    // 바닥에 닿았을 때만 점프
    [SerializeField]
    private LayerMask groundLayer; // 바닥 체크를 위한 충돌 레이어
    private BoxCollider2D boxcoliider2D; // 오브젝트의 충돌 범위 컴포넌트 - 만약 boxCollider로 한다면 BoxCollider2D 로 선언
    [HideInInspector]
    public bool isGrounded; // 바닥 체크 (바닥에 있으면 true)
    private Vector3 footPosition; // 발의 위치

    private Rigidbody2D rigid2D;

    private Animator animator;

    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.

    // 오브젝트가 꺼져있어도 1회 동작
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcoliider2D = gameObject.GetComponent<BoxCollider2D>();
    }


    /* 
       Update는 스크립트가 enabled 상태일때, 매 프레임마다 호출됩니다.
       일반적으로 가장 빈번하게 사용되는 함수이며, 물리 효과가 적용되지 않은 오브젝트의 움직임이나 단순한 타이머, 키 입력을 받을 때 사용됩니다.
    */

    /* 
       프레임을 기반으로 호출되는 Update 와 달리 Fixed Timestep에 설정된 값에 따라 일정한 간격으로 호출
       물리 효과가 적용된(Rigidbody) 오브젝트를 조정할 때 사용됩니다. (Update는 불규칙한 호출임으로 물리엔진 충돌검사 등이 제대로 안될 수 있다)
       물리 연산을 하기 전 실행됨.
    */
    private void FixedUpdate()
    {
        Bounds bounds = boxcoliider2D.bounds; // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보

        footPosition = new Vector2(bounds.center.x, bounds.min.y); // x 가운데 y 맨 밑에

        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer); // 콜라이더가 원형 영역에 속하는지 확인.
    }

    private void OnDrawGizmos() // 기즈모 생성 
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
