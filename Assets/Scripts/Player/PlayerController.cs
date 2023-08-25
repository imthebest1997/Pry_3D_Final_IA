using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private CharacterController characterController;
    [SerializeField] float speed = 10f;
    private readonly float gravity = -9.80f;

    Vector3 velocity = Vector3.zero;
    Vector3 moveDirection;

    bool isGrounded;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] Animator animator;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject playerModel;
    [SerializeField] float rotateSpeed = 5f;

    //Actuar ante una trampa o reduccion de vida
    [SerializeField] bool isKnocking;
    [SerializeField] float knockBackLenght = .5f;
    [SerializeField] float knockBackCounter;
    [SerializeField] Vector2 knockBackPower = new(-3,10);

    //Partes del Robot
    public GameObject[] playerPieces;

    public bool stopMove;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (!isKnocking && !stopMove)
        {
            isGrounded = characterController.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            //Movimiento
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = transform.right * x + transform.forward * z;

            //Salto 
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

            characterController.Move(speed * Time.deltaTime * moveDirection);

            //Rotar al jugador cuando hay movimiento
            if (x != 0 || z != 0)
            {
                transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            velocity.y += gravity * Time.deltaTime;//Gravedad

            characterController.Move(velocity * Time.deltaTime);
        }else if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;
            moveDirection = playerModel.transform.forward * knockBackPower.x;

            velocity.y += gravity * Time.deltaTime;//Gravedad

            characterController.Move(moveDirection * Time.deltaTime);
            
            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if(stopMove)
        {
            moveDirection = Vector3.zero;
            velocity = Vector3.zero;
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", characterController.isGrounded);
    }


    public void KnockBack()
    {
        isKnocking = true;
        knockBackCounter = knockBackLenght;
        moveDirection.y = knockBackPower.y;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
