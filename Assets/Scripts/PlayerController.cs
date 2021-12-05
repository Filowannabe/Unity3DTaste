using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Si tenemos un objeto con este script, automaticamente
//agrega el character_controller
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Los headers implican las categorias donde se comprenden las viarbales para despues interpretarlas en código.
    [Header("References")]
    public Camera playerCamera;

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;

    [Header("Rotation")]
    public float rotationSensibility = 10;

    [Header("Jump")]
    public float jumpHeight = 1.9f;

    private float camereVerticalAngle;
    //vector3.zero inicializa en 0 cada param xyz
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Look();
        Move();
    }

    //lee el imput del user
    private void Move()
    {
        if (characterController.isGrounded)
        {
            // determina moveinput como vector con coordenadas ({1 ó -1 dependiendo si es presionado 'A' ó 'D'},{0},{1 ó -1 dependiendo si es presionado 'W' ó 'S'})
            //de esta manera se controla la posición del personaje
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //el vector de moveinput entre 0 y 1
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            //condición para correr, si es presionada la tecla SHIFT
            if (Input.GetButton("Sprint"))
            {
                //el moveinput (dirección personaje) por un float que determiné la fuerza de velocidad correr
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                //el moveinput (dirección personaje) por un float que determiné la fuerza de velocidad caminar
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                //obtiene la coordenada en Y del moveinput y saca la raiz de la fuerza por la gravedad -2 (siendo este ultimo la distancia del salto),
                // -debido a que la gravedad funciona de manera personalizada-
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }
        //obtiene la coordenada en Y del moveinput y  le suma el float de la fuerza gravitatoria por el deltatime con la finalidad que la gravead sea funcional.
        moveInput.y += gravityScale * Time.deltaTime;
        //mueve al personaje según el moveinput obtenido en la linea 49
        characterController.Move(moveInput * Time.deltaTime);
    }

    //método para establecer la rotación del personaje
    private void Look()
    {
        //rotation input, vector que empieza con coordenadas (0,0,0) en sus ejes
        //rotation input en eje x sera igual a 1 ó -1 dependiendo de la direccion horizontal y vertical del mouse multiplicado por una rotación tipo float personalizada y deltatime
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        //un float que determiné el angulo de la camara que se instancia como el valor float de la rotación en eje Y
        camereVerticalAngle += rotationInput.y;
        //limita el angulo de la camara a estar en un vector comprendido entre el eje Y actual en X, Y = -70 Y Z= 70
        camereVerticalAngle = Mathf.Clamp(camereVerticalAngle, -70, 70);
        //determina el vector rotate del transform del personaje que sera un vector (0,1,0) -solo en eje Y- por la rotación obtenida en x en la linea 83
        transform.Rotate(Vector3.up * rotationInput.x);
        // el vector rotatión del transform no puede ser asignado como new vector 3, por ende es imposible controlar la rotación del personaje sin usar Quaternion.Euler
        playerCamera.transform.localRotation = Quaternion.Euler(-camereVerticalAngle, 0f, 0f);
    }

}
