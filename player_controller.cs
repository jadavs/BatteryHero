using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float m_lookSensitivity = 10.0f;
    public Camera fps_cam;
    private Rigidbody m_Rigid;

    private Vector3 velocity, jump_Velocity;
    private Vector3 p_rotation;
    private Vector3 cam_rotation;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 4f;

	  private float currentCameraRotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //mouse movement
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cam_rot =  new Vector3(xRot, 0, 0) * m_lookSensitivity;
        Vector3 m_rotation = new Vector3(0, yRot, 0) * m_lookSensitivity;
        p_rotation =   m_rotation;
        cam_rotation = -cam_rot;

        //Calculate movement velocity as a 3D vector
    		float xMov = Input.GetAxis("Horizontal");
    		float zMov = Input.GetAxis("Vertical");
        Vector3 move_hor = transform.right * xMov;
        Vector3 move_ver = transform.forward * zMov;
        Vector3 _velocity = (move_hor + move_ver).normalized * movementSpeed;
        Vector3 j_velocity = new Vector3(0,0,0);
        if (Input.GetKeyDown(KeyCode.Space)) {
            j_velocity = Vector3.up * jumpVelocity;
        }
        velocity = _velocity;
        jump_Velocity = j_velocity;
    }

    void FixedUpdate() {
        movePlayer();
        rotatePlayer();
        rotateCamera();

        if (m_Rigid.velocity.y < 0) {
            m_Rigid.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (m_Rigid.velocity.y > 0 && !Input.GetButton("Jump")) {
            m_Rigid.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void movePlayer() {
        if (velocity != Vector3.zero) {
            m_Rigid.MovePosition(m_Rigid.position + velocity * Time.fixedDeltaTime);
        }
        if (jump_Velocity != Vector3.zero) {
            m_Rigid.velocity += jump_Velocity;
        }
    }

    void rotatePlayer() {
        Quaternion rotx = m_Rigid.rotation * Quaternion.Euler(p_rotation);
        m_Rigid.MoveRotation(rotx);
    }

    void rotateCamera() {
        currentCameraRotationX += cam_rotation.x;
        fps_cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }

}
