using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public float batasKanan = 15;
    public float batasKiri = -14.998f;
    public float batasAtas = 11.297f;
    public float batasBawah = -11.194f;

    void LateUpdate()
    {
        // Ambil posisi kamera saat ini
        Vector3 posisiKamera = transform.position;

        // Batasi posisi kamera agar tetap di dalam area yang ditentukan
        posisiKamera.x = Mathf.Clamp(posisiKamera.x, batasKiri, batasKanan);
        posisiKamera.y = Mathf.Clamp(posisiKamera.y, batasBawah, batasAtas);

        // Terapkan posisi baru ke kamera
        transform.position = posisiKamera;
    }
}
