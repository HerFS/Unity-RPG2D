using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; // 맵이 이동되면, 플레이어가 시작될 위치.

    private Movement2D thePlayer;
    private CameraManager theCamera;

    void Awake()
    {
        thePlayer = FindObjectOfType<Movement2D>();
        theCamera = FindObjectOfType<CameraManager>();

        if (startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = this.transform.position;
        }
    }

}
