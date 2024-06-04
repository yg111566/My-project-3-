using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Playerpos;
    public Transform Camerapos;
    public float Camerarepair = 3f;
    public float slowTime = 0.1f;
    private Vector3 velocity = Vector3.zero;
    public Tilemap tilemap; 

    private float camHalfHeight;
    private float camHalfWidth;

    private Bounds tilemapBounds;
    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        // 타일맵의 경계를 계산
        tilemapBounds = tilemap.localBounds;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = Playerpos.position;
        newPosition.z = Camerapos.position.z;
        newPosition.y += Camerarepair;
        newPosition = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, slowTime);
        newPosition.x = Mathf.Clamp(newPosition.x, tilemapBounds.min.x + camHalfWidth, tilemapBounds.max.x - camHalfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, tilemapBounds.min.y + camHalfHeight, tilemapBounds.max.y - camHalfHeight);
        Camerapos.position = newPosition;
    }
}
