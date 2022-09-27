using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class char_controler : MonoBehaviour
{
    [Header("Wallrunning")]
    private bool wall_run;

    //Wallrun
    [Header("Detection")]
    public LayerMask whatIsWall;
    public float wallCheckDistance;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;
    //public float minJumpHeight;

    ////Wallrun^^^^^^^^^^
    public float walkSpeed = 70;
    public float runSpeed = 300;
    public float wallrunSpeed = 400;
    public float gravity = -500;
    public float jumpHeight = 250;
    static bool in_battle = true;
    //sus public 	
    public float dash_speed;
    public float dash_time;
    public float cooldown_Dash = 5;
    private float next_Dash = 0;
    public float cooldown_Dash1 = 5;
    private float next_Dash1 = 0;
    bool can_do_second_dash = false;
    //desh paramater thou^^^^^^
    bool can_double_jump;
    bool space_press;
    //static bool running;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;


    Animator animator;
    Transform cameraT;
    public CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }
    //enemy colider
    //private void OnTriggerEnter(Collider other)
    //{
    //char_controler.in_battle = true;
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    char_controler.in_battle = false;
    //}
    void Update()         
    {
        //animator.SetBool("fire_right", false);
        //animator.SetBool("fire_left", false);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    animator.SetBool("fire_left", false);
        //    animator.SetBool("fire_right", true);
        //    //Debug.Log("Right Mouse Button Clicked");
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
            
        //    animator.SetBool("fire_left", true);
        //    animator.SetBool("fire_right", false);
        //    //Debug.Log("Left Mouse Button Clicked");
        //}
        //Wallrun*CheckingWall
        CheckForWall();
        Wall_run();
        //

        //Ray ray = new Ray(transform.position, transform.forward);       
        //Debug.DrawRay(transform.position + transform.up * 50f, transform.right*10f, Color.yellow);

        // input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);
        //END direction of movement for dash and staf


        //MOvement state run or walk/run dep on battle state
        if (in_battle == true)
        {
            Move(inputDir, true);
        }
        else
        {
            Move(inputDir, running);
        }
        //Double Jump

        if (Input.GetKeyDown(KeyCode.Space) & in_battle == true)
        {
            animator.SetBool("space_on_click", true);
            animator.SetBool("can_secnd", false);
            if (controller.isGrounded)
            {
                //animator.SetBool("space_on_click", true);
                Jump();
                space_press = true;
                can_double_jump = true;
                animator.SetBool("can_secnd", true);
                //animator.SetBool("space_on_click", false);

            }
            else
            {
                if (can_double_jump)
                {
                    //animator.SetBool("space_on_click", true);
                    space_press = true;
                    can_double_jump = false;
                    Jump();
                    animator.SetBool("can_secnd", false);
                    //animator.SetBool("space_on_click", false);
                    //StartCoroutine(NO_Air_Control());
                }
            }
        }

        // animator
        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
        animator.SetFloat("speed_persent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        //defenetion of state ground/air AIR OR NOT AIR OR NOT AIR OR NOT AIR OR NOT AIR OR NOT
        if (controller.isGrounded)
        {
            animator.SetBool("in_Air", false);
            airControlPercent = 0.558f;
            wall_run = false;
            //Debug.Log(airControlPercent);
        }
        else if (!controller.isGrounded)
        {
            if (wallLeft)
            {
                animator.SetBool("wall_left", true);
                animator.SetBool("wall_right", false);
            }
            else if (wallRight)
            {
                animator.SetBool("wall_right", true);
                animator.SetBool("wall_left", false);
            }
            else
            {
                animator.SetBool("wall_left", false);
                animator.SetBool("wall_right", false);
            }
            //Invoke ("Cant_move_air", 1);
            animator.SetBool("in_Air", true);
            wall_run = true;

            //StartCoroutine(NO_Air_Control());
            //Debug.Log(airControlPercent);

        }

        //Debug.Log(on_ground);
        //DASH/////DASH///////////////DASH////////////////DASH/////////

        if (Time.time > next_Dash | Time.time > next_Dash1)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) & in_battle == true)
            {

                if (Time.time > next_Dash & !can_do_second_dash)
                {
                    //Debug.Log("rab1");
                    StartCoroutine(Dash());
                    next_Dash = Time.time + cooldown_Dash;
                    can_do_second_dash = true;
                    next_Dash = Time.time + cooldown_Dash;
                    //StartCoroutine(NO_Air_Control());
                }

                else if (Time.time > next_Dash1 & can_do_second_dash)
                {
                    //Debug.Log("rab2");
                    StartCoroutine(Dash1());
                    next_Dash1 = Time.time + cooldown_Dash1;
                    can_do_second_dash = false;
                    next_Dash1 = Time.time + cooldown_Dash1;
                    //StartCoroutine(NO_Air_Control());
                }


            }
        }

        //DASH/////DASH///////////////DASH////////////////DASH/////////
    }
    void FixedUpdate()
    {
        animator.SetBool("space_on_click", false);
        animator.SetBool("dash", false);
        //Debug.Log(airControlPercent);
    }
    //WALL RUN VARIBLE////////////WALL RUN VARIBLE/////////1)CHECKING THE WALL//////WALL RUN VARIBLE/////////
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }
  

    //Actual WALL RUN MOVEMENT STETMENT////////////////////////////////////////////////////
    private void Wall_run()
    {
       

        if (wall_run & in_battle & (wallLeft || wallRight) & (Input.GetAxisRaw("Vertical") > 0) & !controller.isGrounded)
        {
            velocityY = 0;
            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
            Vector3 wallForward = wallRight ? Vector3.Cross(wallNormal, -transform.up) : Vector3.Cross(wallNormal, transform.up);
            //Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);//лево
            //Vector3 wallForward = Vector3.Cross(wallNormal, -transform.up);//право
            //Debug.Log(wallForward);
            controller.Move(wallForward * wallrunSpeed * Time.deltaTime);
            //if ((transform.forward - wallForward).magnitude > (transform.forward - -wallForward).magnitude)
            //    wallForward = -wallForward;
            //Debug.Log("WR work");

        }

    }

    private void End_Wall_run()
    {
        wall_run = false;
    }
    //End of Actual WALL RUN MOVEMENT STETMENT/////////

    //void Cant_move_air()
    //{
    //    airControlPercent = 0f; 
    //}
    void Move(Vector2 inputDir, bool running)
    {

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

    }
    //DASH FUNCTION DASH FUNCTION DASH FUNCTION DASH FUNCTION DASH FUNCTION
    IEnumerator Dash()
    {
        animator.SetBool("dash", true);
        float starTime = Time.time;
        while (Time.time < starTime + dash_time)
        {
            Vector3 velocity = transform.forward * dash_speed + Vector3.up /** velocityY*/;
            controller.Move(velocity * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator Dash1()
    {
        animator.SetBool("dash", true);
        float starTime1 = Time.time;
        while (Time.time < starTime1 + dash_time)
        {
            Vector3 velocity = transform.forward * dash_speed + Vector3.up /** velocityY*/;
            controller.Move(velocity * Time.deltaTime);
            yield return null;
        }
    }
    //IEnumerator NO_Air_Control()
    //{
    //    if (airControlPercent > 0f & !controller.isGrounded)
    //    {
    //        Debug.Log("ya rabotuy");
    //        airControlPercent = 0.518f;
    //        yield return new WaitForSeconds(0.5f);
    //        airControlPercent = 0f;
    //    }
    //}
    //jump function for 1st and 2nd jump
    void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
        velocityY = jumpVelocity;
        space_press = false;
    }

    //160.164
    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }


}
