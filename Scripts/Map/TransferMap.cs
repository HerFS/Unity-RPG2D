using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름
    //public Scene scene;

    public Transform targert;

    private Movement2D thePlayer;
    private CameraManager theCamera;

    public BoxCollider2D targetBound;

    private void Awake()
    {
        thePlayer = FindObjectOfType<Movement2D>(); // 다수의 객체
        theCamera = FindObjectOfType<CameraManager>();
        // GetComponent // 단일 객체
    }

    private void OnTriggerStay2D(Collider2D collision) // rigidbody2d 에서 Sleeping 모드를 변경 -> never sleep 으로
    {
        if (collision.gameObject.name == "Player") // 오브젝트의 이름이 Player 라면
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                thePlayer.currentMapName = transferMapName;
                theCamera.SetBound(targetBound);
                theCamera.transform.position = new Vector3(targert.transform.position.x, targert.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = targert.transform.position;
            }
        }
    }
}
