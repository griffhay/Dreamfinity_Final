using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

[RequireComponent(typeof(AudioSource))]



public class CharacterControler : MonoBehaviour {

    
	private Rigidbody r_rigBodRef;  // Refferance to the rigidbody Componenet
    public float m_groundCheckDistance;  // The distace from the center of the character downward to check for
    //the ground. Needs to be at least half the characters height if the origin is in the center of the character;
	float m_origonalGroundCheckDistance; // This holds the origonal check distance to so that it may be reset once changed.
    // This is so the variable may be set from the inspecter;
	Vector3 m_groundNormal;  // The slope of the ground under the player
	public bool m_isGrounded;   // Weather the player is on the ground or not
	float m_sideWayesAmount, m_forwardAmount;    // Amount of movement in the respective direction vb v bn  
    public bool m_isJumping;
	Vector3 m_leanTorque; // The direction of lean 
	float m_leanAmount;
    float originalStandSpeed; // the origonal value entered in to stand up speed;


    CapsuleCollider r_capColider;
    [Header("Character Settings")]

    [Tooltip("This value will be added to the velocity when the player moves")]
    public float acceleration; // How fast the character speeds up to top speed
    [Tooltip("This is the amount of power added to the velocity of the player in the up direction")]
    public float m_jumpPower;   // How much force is used
    
    float m_crouchSpeed;  // Speed bwtween standing and crouching, when the space bare is pressed
    float m_StandUpSpeed; // Speed between crouching and standing, when the space is released
    bool m_canJump; 
    bool m_standing; // This is true while the player is standing up
    public bool CombatFaceing = true;
    Camera m_camera;

    //Dash Variables
    public bool m_dashing;
    float timer;
    public float m_dashSpeed;
    public float m_dashDir;

    public Vector3 out_LookCamFoward;
    public float m_dashForce;
    

    void Start ()
    {

        if (acceleration == 0)
        {
            acceleration = 10;
        }

        if(m_jumpPower == 0)
        {
            m_jumpPower = 10;
        }

        //float halfColHight = GetComponent<CapsuleCollider>().height / 2;
        //halfColHight += .1f;
        //m_groundCheckDistance = halfColHight;

        originalStandSpeed = m_StandUpSpeed;
		r_rigBodRef = GetComponent<Rigidbody>();
        r_capColider = GetComponent<CapsuleCollider>();
		m_origonalGroundCheckDistance = m_groundCheckDistance;
        m_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

    private void Update()
    {
        ToggleCharacterlook();
    }

    public void ToggleCharacterlook()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            if(CombatFaceing)
            {
                CombatFaceing = false;
            }
            else
            {
                CombatFaceing = true;
            }
        }
    }

    public void Move(Vector3 move, bool crouch, bool jump)
	{
        if (move.magnitude > 1f)
        {
            move = move.normalized;
        }

		Grounded();

		move = Vector3.ProjectOnPlane(move, m_groundNormal);
        m_forwardAmount = move.z;// * r_capColider.height;
		m_sideWayesAmount = move.x;
		HandleGroundMovement(jump,crouch);
	}

    //public void Dash(float inputDir)
    //{
    //    m_dashing = true;
    //}

	void FixedUpdate()
	{

        // Moving the player via the rigidbodys Velocity rather than a transform.Translate(ask me why if you want a better answer)
        r_rigBodRef.velocity = new Vector3(m_sideWayesAmount * acceleration, r_rigBodRef.velocity.y, m_forwardAmount * acceleration);

        //if (m_dashing)
        //{
          
        //    r_rigBodRef.AddForce(transform.right * m_dashDir * 1000 , ForceMode.Impulse);
        //}
        
        // Getting the x and z velocity as a direction
        Vector3 lookVelocity = new Vector3(r_rigBodRef.velocity.x, 0, r_rigBodRef.velocity.z).normalized;
        Vector3 lookCamFoward = new Vector3(m_camera.transform.forward.x, 0, m_camera.transform.forward.z).normalized;
        // Quaturnians can not be 0 (the world would explode as said by Leonhard Euler(Master of rotation))	
        if (lookVelocity != Vector3.zero)
		{
            if(!CombatFaceing)
            {
                // Still needs to be smoothed
                r_rigBodRef.rotation = Quaternion.LookRotation(lookVelocity, Vector3.up);
            }
            else
            {
                r_rigBodRef.rotation = Quaternion.LookRotation(lookCamFoward, Vector3.up);
            }
           
		}
	}


    void HandleGroundMovement(bool jump, bool crouch)
    {
        //if the player is grounded and the press the jump button, they jump and isGrounded is again false!
            
            if (m_isGrounded && jump)
            {
            m_isJumping = true;
                r_rigBodRef.velocity = new Vector3(r_rigBodRef.velocity.x, m_jumpPower, r_rigBodRef.velocity.z);
                m_isGrounded = false;
                m_groundCheckDistance = m_origonalGroundCheckDistance;
                //play audio
                AudioSource audio = GameObject.FindWithTag("AudioJump").GetComponent<AudioSource>();
                audio.Play();
                
            }

            if (crouch)
            {
                r_capColider.height = Mathf.Lerp(r_capColider.height, 1f, Time.deltaTime * m_crouchSpeed);
                if (r_capColider.height > 1f && r_capColider.height < 1.15)
                {
                    r_capColider.height = 1f;
                }
            }
            else
            {
                r_capColider.height = Mathf.Lerp(r_capColider.height, 2f, Time.deltaTime * m_StandUpSpeed);

                if (r_capColider.height < 2 && r_capColider.height > 1.85)
                {
                    r_capColider.height = 2f;
                }
            }

            //this.transform.localScale = new Vector3(1, r_capColider.height / 2f, 1f);
    }

	void HandleAirbornMovement()
	{
        // This is fancey syntax that says if the characters y velocity is less than 0 (or moving down) then 
        // groundCheckDistance is equal to its origonal value. If the player is not moving down, then ground check distance
        // is equal to 0.01f or a float that is 0.01
		m_groundCheckDistance = r_rigBodRef.velocity.y < 0 ? m_origonalGroundCheckDistance : 0.01f;
	}

    //This Function checks to see if the player is on the ground
	public void Grounded()
	{
		RaycastHit hitInfo; // A variable that holds information on the object collider the raycast hits
		Debug.DrawRay(transform.position, Vector3.down); // A visualization of the ray in the scene view

        //This is an if statement that is true if the raycast hits anything other than the player character.
        // A ray from the center of the player, in the direction down (0,-1,0), that outputs hit info and that is as long 
        // as our ground check distance variable 

		if (Physics.Raycast(transform.position , Vector3.down, out hitInfo, m_groundCheckDistance))
		{
			m_groundNormal = hitInfo.normal;
			m_isGrounded = true;
            m_isJumping = false;
            m_StandUpSpeed = originalStandSpeed;
		}
		else
		{
			m_isGrounded = false;
			m_groundNormal = Vector3.up;
		}
	}
}
