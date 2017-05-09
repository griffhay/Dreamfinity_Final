using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class CharacterControler : MonoBehaviour {

    CapsuleCollider r_capColider;
    Rigidbody rigBodRef;
    Camera m_camera;

    private Vector3 m_groundNormal;  // The slope of the ground under the player
    public bool CombatFaceing = true;
    [HideInInspector]
    public bool m_dashing;     //True when player is dashing

    //Lean into movement variabls
    Vector3 m_leanTorque; // The direction of lean 
	float m_leanAmount;
    float originalStandSpeed; // the origonal value entered in to stand up speed;

    //Jumping Variables
    [Header("Jumping Variables")]
    [Tooltip("This is the amount of power added to the velocity of the player in the up direction")]
    public float m_jumpPower;                   // How much force is used
    [Tooltip("The length of the jump while the jump button is being held")]
    public int m_jumpDeration;
    int m_jumpCounter;                          //holds the incrementation while the jump input is held
    bool m_canJump;                             //allows continuing jumping while true. False when the jump is let up after jumping
    [HideInInspector]public bool m_isJumping;   //true when the player is jumping
    public bool m_isGrounded;  // Weather the player is on the ground or not
    public float m_groundCheckDistance;         // The distace from the center of the character downward to check for the ground. Needs to be at least half the characters height if the origin is in the center of the character;
    float m_origonalGroundCheckDistance;        // This holds the origonal check distance to so that it may be reset once changed.This is so the variable may be set from the inspecter;

    [Header("Movement Variablse")]
    [Tooltip("This value will be added to the velocity when the player moves")]
    public float acceleration;                  //How fast the character speeds up to top speed
    [Tooltip("The maximum speed the player will accelorate to")]
    public float accerationMax;                 //The maximum speed the player will accelorate to
    [Tooltip("The amount per frame the acceloration will grow. This value is product of the time since the last frame and this number.")]
    public float accelStep;                     //The amount per frame the acceloration will grow. This value is product of the time since the last frame and this number.
    float m_sideWayesAmount, m_forwardAmount;   //Amount of movement in the respective direction

    //Animation Intervention Controls
    [HideInInspector]
    public bool isAttacking;
    AttackAnimEvent attackAnimEventRef;    

    void Start ()
    {

        /*Used to increment acceleration */
         
        if (acceleration == 0)
        {
            acceleration = 10;
        }

        if(m_jumpPower == 0)
        {
            m_jumpPower = 10;
        }
        

		rigBodRef = GetComponent<Rigidbody>();
        r_capColider = GetComponent<CapsuleCollider>();
		m_origonalGroundCheckDistance = m_groundCheckDistance;
        m_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        attackAnimEventRef = this.gameObject.GetComponent<AttackAnimEvent>();
    }

    public void ToggleCharacterlook(bool toggle)
    {

        CombatFaceing = toggle;
        
    }

    public void Move(Vector3 move, bool crouch, bool jump)
	{
        if (move.magnitude > 1f)
        {
            move = move.normalized;
        }

        if (move.magnitude > 0)
        {
            if (acceleration + accelStep * Time.deltaTime < accerationMax)
            {
                acceleration += accelStep * Time.deltaTime;
            }
        }
        else
        {
            acceleration = 5;
        }

        Grounded();

		move = Vector3.ProjectOnPlane(move, m_groundNormal);
        m_forwardAmount = move.z;
		m_sideWayesAmount = move.x;
		HandleGroundMovement(jump,crouch);
	}

	void FixedUpdate()
	{
        // Moving the player via the rigidbodys Velocity rather than a transform.Translate(ask me why if you want a better answer)
        if (attackAnimEventRef.isAttacking)
        {
            rigBodRef.isKinematic = true;
            rigBodRef.velocity = Vector3.zero;
            Debug.Log("Hope This works");

        }
        else
        {
            rigBodRef.isKinematic = false;
            rigBodRef.velocity = new Vector3(m_sideWayesAmount * acceleration, rigBodRef.velocity.y, m_forwardAmount * acceleration);


            //if (attackAnimEventRef.startStep)
            //{
            //    rigBodRef.velocity = rigBodRef.transform.forward * 3.5f;
            //}
        }

        // Getting the x and z velocity as a direction
        Vector3 lookVelocity = new Vector3(rigBodRef.velocity.x, 0, rigBodRef.velocity.z).normalized;
        Vector3 lookCamFoward = new Vector3(m_camera.transform.forward.x, 0, m_camera.transform.forward.z).normalized;
        Quaternion finalLook = Quaternion.LookRotation(lookCamFoward, Vector3.up);

        // Quaturnians can not be 0 (the world would explode as said by Leonhard Euler(Master of rotation))	
        if (lookVelocity != Vector3.zero)
		{
            if(!CombatFaceing)
            {
                // Still needs to be smoothed
                rigBodRef.rotation = Quaternion.LookRotation(lookVelocity, Vector3.up);
            }
            else
            {
                rigBodRef.rotation = Quaternion.LookRotation(lookCamFoward, Vector3.up);
            }
		}
	}

    void HandleGroundMovement(bool jump, bool crouch)
    {
        //if the player is grounded and the press the jump button, they jump and isGrounded is again false!
            
        if (m_isGrounded && jump)
        {
            m_isJumping = true;
            //rigBodRef.velocity = new Vector3(rigBodRef.velocity.x, m_jumpPower , rigBodRef.velocity.z);
            rigBodRef.AddForce(Vector3.up * m_jumpPower, ForceMode.Force);
            m_isGrounded = false;
            m_groundCheckDistance = m_origonalGroundCheckDistance;
            m_canJump = true;
                
        }

        if(m_isJumping && jump && m_jumpCounter < m_jumpDeration && m_canJump)
        {
            //rigBodRef.velocity = new Vector3(rigBodRef.velocity.x, m_jumpPower, rigBodRef.velocity.z);
            rigBodRef.AddForce(Vector3.up * m_jumpPower, ForceMode.Force);
            m_jumpCounter++;
        }

        if (m_isJumping && !jump)
        {
            m_canJump = false;
        }
        
        if(m_isGrounded)
        {
            m_jumpCounter = 0;
        }
    }

	void HandleAirbornMovement()
	{
        // if player falling, the groundcheck distance is the origonal value, else, its 0.01f
		m_groundCheckDistance = rigBodRef.velocity.y < 0 ? m_origonalGroundCheckDistance : 0.01f;

        if(rigBodRef.velocity.y > 0 && m_isJumping == false)
        {
            rigBodRef.drag = 5;
        }
        else
        {
            rigBodRef.drag = 0;
        }
	}

    //This Function checks to see if the player is on the ground
	public void Grounded()
	{
		RaycastHit hitInfo; // A variable that holds information on the object collider the raycast hits
		Debug.DrawRay(transform.position, Vector3.down); // A visualization of the ray in the scene view

		if (Physics.Raycast(transform.position , Vector3.down, out hitInfo, m_groundCheckDistance))
		{
			m_groundNormal = hitInfo.normal;
			m_isGrounded = true;
            m_isJumping = false;
        }
		else
		{
			m_isGrounded = false;
			m_groundNormal = Vector3.up;
		}
	}
}
