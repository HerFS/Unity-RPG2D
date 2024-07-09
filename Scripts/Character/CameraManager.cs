using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 대상을 쫓을 건지
    private Vector3 targetPosition; // 대상의 현재 위치 값

    //static public CameraManager Cinstance;

    // boxcollider 영역의 최소 최대 xyz값, 너비, 높이 값을 지님
    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    // 카메라의 높이값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;

    private void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; // 반너비를 구하는 공식
    }

    // Update is called once per frame
    private void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y + 1f, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);  // Clamp(vlaue, min, max) 
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);  // Clamp(vlaue, min, max)

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z); // 이렇게 this 를 생략해도됨
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
