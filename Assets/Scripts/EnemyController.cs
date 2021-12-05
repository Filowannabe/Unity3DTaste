using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed;
    public Camera playerCamera;
    public float rotationSensibility = 10;
    public float gravityScale = -20f;
    CharacterController characterController;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    public GameObject player;
    public bool isWatching = true;
    private float camereVerticalAngle;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isWatching)
        {
           // Look();
            Move();

        }
    }

    private void Move()
    {

        if (characterController.isGrounded)
        {
            moveInput = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);

            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
        }
        moveInput = transform.TransformDirection(moveInput) * walkSpeed;
        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {

        direction = player.transform.position - transform.position;

     /*   Debug.Log(direction);
        if (direction.z >= 0 && direction.x >= 0)
        {
            Debug.Log("DERECHA");
            transform.rotation = Quaternion.Euler(0f, 81.77f, 0f);
        }
        else if (direction.z < 0 && direction.x <= 0)
        {
            Debug.Log("IZQUIERDA");
            transform.rotation = Quaternion.Euler(0f, -81.77f, 0f);
        }

        else if (direction.z >= 0 && direction.x <= 0)
        {
            Debug.Log("ADELANTE");
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (direction.z <= 0 && direction.x >= 0)
        {
            Debug.Log("ATRAS");
            transform.rotation = Quaternion.Euler(0f, -191.96f, 0f);
        }*/

        //rotationInput.x = direction.x * Time.deltaTime;

        //camereVerticalAngle = Mathf.Clamp(camereVerticalAngle, -70, 70);


        //if (direction.z >= 0) transform.Rotate(new Vector3(0f, player.transform.position.y, 0f));
        //else transform.Rotate(new Vector3(0f, player.transform.position.y * -1, 0f));


        //playerCamera.transform.localRotation = Quaternion.Euler(-camereVerticalAngle, 0f, 0f);
    }
}
