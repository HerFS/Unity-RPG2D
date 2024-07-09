using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�
    //public Scene scene;

    public Transform targert;

    private Movement2D thePlayer;
    private CameraManager theCamera;

    public BoxCollider2D targetBound;

    private void Awake()
    {
        thePlayer = FindObjectOfType<Movement2D>(); // �ټ��� ��ü
        theCamera = FindObjectOfType<CameraManager>();
        // GetComponent // ���� ��ü
    }

    private void OnTriggerStay2D(Collider2D collision) // rigidbody2d ���� Sleeping ��带 ���� -> never sleep ����
    {
        if (collision.gameObject.name == "Player") // ������Ʈ�� �̸��� Player ���
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
