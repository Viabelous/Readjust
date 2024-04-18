using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Objek target yang diikuti oleh kamera
    public Vector3 offset; // Jarak relatif dari kamera ke target

    public float minX; // Batas minimum pergerakan kamera di sumbu X
    public float maxX; // Batas maksimum pergerakan kamera di sumbu X
    public float minY; // Batas minimum pergerakan kamera di sumbu Y
    public float maxY; // Batas maksimum pergerakan kamera di sumbu Y


    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        // Hitung posisi target yang diikuti oleh kamera
        Vector3 desiredPosition = target.position + offset;

        // Batasi posisi target di antara batas minimum dan maksimum pada sumbu X
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        // Batasi posisi target di antara batas minimum dan maksimum pada sumbu Y
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Atur posisi kamera menjadi posisi yang sudah diinterpolasi
        transform.position = desiredPosition;

        // Atur rotasi kamera agar selalu menghadap target
        // transform.LookAt(target);
    }

}
