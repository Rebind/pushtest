using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    private float jumpHeight;

    private float timeToJumpApex = .4f;

    private float moveSpeed;
    private bool facingRight;
    private float gravity;

    private float jumpVelocity;

    private Vector3 playerVelocity;
    private BoxCollider2D myBoxcollider;
    private Controller2D myController;
    private Animator myAnimator;
    private bool canJump;
   private int state;


    void Start()
    {
        moveSpeed = 10f;
        jumpHeight = 3f;
        facingRight = true;
        myBoxcollider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
        myAnimator = GetComponent<Animator>();
        myController = GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
    }

    void Update()
    {
        state = myAnimator.GetInteger("state");
        HandleMovments();
        Flip();
        HandleInputs();
        handleBodyCollisions();
        handleBuffsDebuffs();
        
    }

    private void HandleMovments()
    {
        if (myController.collisions.above || myController.collisions.below)
        {
            playerVelocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //get input from the player (left and Right Keys)

        if (Input.GetKeyDown(KeyCode.Space) && myController.collisions.below && myAnimator.GetInteger("state") != 0)  //if spacebar is pressed, jump
        {
            playerVelocity.y = jumpVelocity;
        }
        playerVelocity.x = input.x * moveSpeed;

        playerVelocity.y += gravity * Time.deltaTime;
        myController.Move(playerVelocity * Time.deltaTime);
        myAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (myAnimator.GetFloat("speed") != 0)
        {
            myAnimator.SetBool("isMoving", true);
        }
        else
        {
            myAnimator.SetBool("isMoving", false);
        }
    }

    private void handleBuffsDebuffs()
    {
    
        if (state == 1 || state == 2 || state == 3)
        {
            moveSpeed = 5f;
          //  jumpHeight = 3f;
        }
        else if (state == 4 || state == 6 || state == 8)
        {
            moveSpeed = 7.5f;
           // jumpHeight = 6f;
        }
        else if (state == 7 || state == 5 || state == 9)
        {
            moveSpeed = 12.5f;
           // jumpHeight = 9f;
        }
        else
        {
            moveSpeed = 10f;
        }
    }

    private void Flip()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0 && !facingRight || horizontal<0 && facingRight)
         {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
          }
        }

    private void HandleInputs()
    {

    }

    private void handleBodyCollisions()
    {
        if (myAnimator.GetInteger("state") == 0)
        {
            helperBoxCollider(0f, "offset", "y");
            helperBoxCollider(2.29f, "size", "y");
            helperBoxCollider(0f, "offset", "x");
            helperBoxCollider(2.1f, "size", "x");
            myController.horizontalRayCount = 4;
            myController.verticalRayCount = 4;
        }
        else if(myAnimator.GetInteger("state") == 1)
        {
            helperBoxCollider(1.45f, "offset", "y");
            helperBoxCollider(4.78f, "size", "y");
            helperBoxCollider(0f, "offset", "x");
            helperBoxCollider(2.1f, "size", "x");
            myController.horizontalRayCount = 7;
            myController.verticalRayCount = 4;
        }
         else if (myAnimator.GetInteger("state") == 2)
         {
             helperBoxCollider(1.45f, "offset", "y");
             helperBoxCollider(4.78f, "size", "y");
             helperBoxCollider(-0.11f, "offset", "x");
             helperBoxCollider(2.62f, "size", "x");
            myController.horizontalRayCount = 7;
            myController.verticalRayCount = 5;
        }
         else if (myAnimator.GetInteger("state") == 3)
         {
             helperBoxCollider(1.45f, "offset", "y");
             helperBoxCollider(4.78f, "size", "y");
             helperBoxCollider(0.04f, "offset", "x");
             helperBoxCollider(3.01f, "size", "x");
            myController.horizontalRayCount = 7;
            myController.verticalRayCount = 6;
        }
         else if (myAnimator.GetInteger("state") == 4)
         {
            helperBoxCollider(3.53f, "offset", "y");
            helperBoxCollider(8.38f, "size", "y");
            helperBoxCollider(0.02f, "offset", "x");
            helperBoxCollider(2.73f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
         else if (myAnimator.GetInteger("state") == 5)
         {
            helperBoxCollider(3.53f, "offset", "y");
            helperBoxCollider(8.38f, "size", "y");
            helperBoxCollider(0.02f, "offset", "x");
            helperBoxCollider(2.73f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
         else if (myAnimator.GetInteger("state") == 6)
         {
             helperBoxCollider(3.53f, "offset", "y");
             helperBoxCollider(8.38f, "size", "y");
             helperBoxCollider(0f, "offset", "x");
             helperBoxCollider(2.1f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
         else if (myAnimator.GetInteger("state") == 7)
         {
             helperBoxCollider(3.53f, "offset", "y");
             helperBoxCollider(8.38f, "size", "y");
             helperBoxCollider(0f, "offset", "x");
             helperBoxCollider(2.1f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
        else if (myAnimator.GetInteger("state") == 8)
         {
            helperBoxCollider(3.53f, "offset", "y");
            helperBoxCollider(8.38f, "size", "y");
            helperBoxCollider(-0.19f, "offset", "x");
            helperBoxCollider(2.31f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
        else if (myAnimator.GetInteger("state") == 9)
         {
            helperBoxCollider(3.53f, "offset", "y");
            helperBoxCollider(8.38f, "size", "y");
            helperBoxCollider(-0.19f, "offset", "x");
            helperBoxCollider(2.31f, "size", "x");
            myController.horizontalRayCount = 12;
            myController.verticalRayCount = 6;
        }
    }

    private void helperBoxCollider(float someFloat, string type, string var)
    {
        if (type.Equals("size"))
        {
            Vector3 size = myBoxcollider.size;
            if (var.Equals("x"))
            {
                size.x = someFloat;
            }
            else if (var.Equals("y"))
            {
                size.y = someFloat;
            }
            myBoxcollider.size = size;
        }
        else if (type.Equals("offset"))
        {
            Vector3 offset = myBoxcollider.offset;
            if (var.Equals("x"))
            {
                offset.x = someFloat;
            }
            else if (var.Equals("y"))
            {
                offset.y = someFloat;
            }
            myBoxcollider.offset = offset;
        }
    }

}
