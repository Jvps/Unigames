using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [Header("System")]
    public Camera cam;

    //Privates
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    /* Receber as variáveis que estavam privadas, tipo um get e set */
    public void Move(Vector3 _velocity) {
        velocity = - _velocity;
    }
    public void Rotate(Vector3 _rotation) {
        rotation = _rotation;
    }
    public void RotationCamera(Vector3 _cameraRotation) {
        cameraRotation = _cameraRotation;
    }

    void FixedUpdate() {
        PlayRotation();
        PlayMoviment();
    }

    // Esse método faz a rotação da câmera
    void PlayRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null) {
            cam.transform.Rotate(-cameraRotation);
        }
    }

    void PlayMoviment() {
        if (velocity != Vector3.zero) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
