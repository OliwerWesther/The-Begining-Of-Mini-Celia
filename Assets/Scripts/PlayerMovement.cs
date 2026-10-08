using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 lastMovement;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //processing Inputs
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        //Physics Calculations
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal",moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMovement.x = moveDirection.x;
            lastMovement.y = moveDirection.y;


            animator.SetFloat("Last_Horizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("Last_Vertical", Input.GetAxisRaw("Vertical"));
        }
        else
        {
            animator.SetFloat("Last_Horizontal", lastMovement.x);
            animator.SetFloat("Last_Vertical", lastMovement.y);
        }


        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
