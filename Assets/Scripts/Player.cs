using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Attack Settings")]
    public Animator animator;        // The Animator controlling attack animation
    public Collider clawCollider;    // The collider on the claws (set as Trigger)
    public float attackDuration = 0.4f; // How long the attack lasts
    public float attackCooldown = 0.8f; // Time between attacks

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canAttack = true;
    private bool isAttacking = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // Make sure claw collider starts disabled so it doesn't hit enemies passively
        if (clawCollider != null)
            clawCollider.enabled = false;
    }

    private void Update()
    {
        //                                  MOVEMENT CODE START
        // -----------------------------------------------------------------------------------
        if (!isAttacking) // prevent movement during attack
        {
            //Ground check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;//Keeps player grounded (wip)
            }

            //Movement Input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //Sprinting
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            //Moving the Player
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //Applying Gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        // -----------------------------------------------------------------------------------
        //                                  MOVEMENT CODE END


        //                                  ATTACK CODE START
        // -----------------------------------------------------------------------------------
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attack());
        }
        // -----------------------------------------------------------------------------------
        //                                  ATTACK CODE END
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        // Play animation
        if (animator != null)
            animator.SetTrigger("Attack");

        // Enable claw collider to detect hits
        if (clawCollider != null)
            clawCollider.enabled = true;

        // Wait for attack animation hit duration
        yield return new WaitForSeconds(attackDuration);

        // Disable claw collider again
        if (clawCollider != null)
            clawCollider.enabled = false;

        // End of attack animation
        isAttacking = false;

        // Wait for cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
