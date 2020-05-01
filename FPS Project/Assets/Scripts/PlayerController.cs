using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [Header("Player Config")]
    public float speed = 5f;
    public float sensibility = 3f;
    public bool enableMouse = true;
    /* enableMouse é por exemplo: Enquanto está atirando,
     * o mouse fica desativado (false), aí se você abre um inventário
     * por exemplo, ou mouse aparece (true). */

    //Private   
    private PlayerMotor motor;

    void Start() {
        motor = GetComponent<PlayerMotor>();
    }
    
    void Update() {
        //MouseLock
        if (enableMouse == true) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Moviment
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _MoveHorizontal = transform.right * _xMov;
        Vector3 _MoveVertical = transform.forward * _zMov;

        //Place Moviment
        Vector3 _velocity = (_MoveHorizontal + _MoveVertical).normalized * speed;
        motor.Move(_velocity);

        //Rotation
        float _yMouse = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yMouse, 0f) * sensibility;
        if (enableMouse == true) {
            motor.Rotate(_rotation);
        }

        //Rotation Camera
        float _xMouse = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xMouse, 0f, 0f) * sensibility;
        if (enableMouse ==  true) {
            motor.RotationCamera(_cameraRotation);
        }
    }
}
