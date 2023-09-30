//#define USE_PLAYER_ASSIST_LOGIC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    enum PlayerState
    {
        None,
        Idle,
        Jumping,
        DoubleJumping,
        Falling
    }

    PlayerState myPlayerState = PlayerState.None;

    Rigidbody myRigidBody;
    public float maxRunSpeed = 5.0f;
    public float runForce = 100.0f;
    public float runDragStrength = 0.1f;
    public float jumpForce = 1200.0f;
    public float onGroundHeight = 1.0f;

    float distanceToGround = 0.0f;

    public GameObject visualBodyRoot;
    bool flipVisual = false;
    public float flipRotationSpeed = 4.0f;

    public bool canDoubleJump = true;

#if USE_PLAYER_ASSIST_LOGIC
    public float fallJumpGraceSeconds = 0.5f;
    public float fallJumpAllowanceHeight = 1.2f;
    float secondsSinceOnGround = 0.0f;
#endif

    Vector3 startingPosition;
    
    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        SetPlayerState(PlayerState.Falling);

        startingPosition = transform.position;
    }
	
	void FixedUpdate () {

        //Calculate distance to ground
        {
            RaycastHit hitInfo;
            Physics.Raycast(transform.position, -Vector3.up, out hitInfo);
            distanceToGround = hitInfo.distance;
        }

        float horizontalAxis = Input.GetAxis("Horizontal");

        float horizontalSpeed = Mathf.Abs(myRigidBody.velocity.x);
        if(horizontalSpeed < maxRunSpeed)
        {
            myRigidBody.AddForce(Vector3.right * horizontalAxis * runForce);
        }

        float clampedVelocityX = myRigidBody.velocity.x * (1.0f - runDragStrength);
        clampedVelocityX = Mathf.Clamp(clampedVelocityX, -maxRunSpeed, maxRunSpeed);

        myRigidBody.velocity = new Vector3(
            clampedVelocityX,
            myRigidBody.velocity.y,
            myRigidBody.velocity.z
        );

        if(clampedVelocityX > 0.25f)
        {
            flipVisual = false;
        }
        else if(clampedVelocityX < -0.25f)
        {
            flipVisual = true;
        }
    }
  
#if USE_PLAYER_ASSIST_LOGIC
    void AssistedPlayerStateChangeCode()
    {
        if(!IsOnGround())
        {
            secondsSinceOnGround += Time.deltaTime;
        }
        else
        {
            secondsSinceOnGround = 0.0f;
        }

        switch (myPlayerState)
        {
            case PlayerState.Idle:
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                else if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Jumping:
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Falling:
                if (IsOnGround())
                {
                    SetPlayerState(PlayerState.Idle);
                }
                else if(Input.GetButtonDown("Jump") && secondsSinceOnGround < fallJumpGraceSeconds)     //Grace Period Jump
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                else if (Input.GetButtonDown("Jump") && distanceToGround < fallJumpAllowanceHeight)     //Allow jump slightly before hitting ground
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                break;
        }
    }
#else
    void StandardPlayerStateChangeCode()
    {
        switch (myPlayerState)
        {
            case PlayerState.Idle:
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.Jumping);
                }
                else if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Jumping:
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                if (Input.GetButtonDown("Jump"))
                {
                    SetPlayerState(PlayerState.DoubleJumping);
                }
                break;
            case PlayerState.DoubleJumping:
                if (IsFalling())
                {
                    SetPlayerState(PlayerState.Falling);
                }
                break;
            case PlayerState.Falling:
                if (IsOnGround())
                {
                    SetPlayerState(PlayerState.Idle);
                }
                if (Input.GetButtonDown("Jump") && canDoubleJump)
                {
                    SetPlayerState(PlayerState.DoubleJumping);
                }
                break;
        }
    }
#endif

    void Update()
    {
        float targetBodyRotY = flipVisual ? 180.0f : 0.0f;
        float currentBodyRotY = visualBodyRoot.transform.localRotation.eulerAngles.y;
        currentBodyRotY += (targetBodyRotY - currentBodyRotY) * flipRotationSpeed * Time.deltaTime;

        visualBodyRoot.transform.localRotation = Quaternion.Euler(
            0.0f,
            currentBodyRotY,
            0.0f
            );

#if USE_PLAYER_ASSIST_LOGIC
        AssistedPlayerStateChangeCode();
#else
        StandardPlayerStateChangeCode();
#endif
    }

    void SetPlayerState(PlayerState newState)
    {
        if(newState != myPlayerState)
        {
            myPlayerState = newState;
            switch(myPlayerState)
            {
                case PlayerState.Idle:
                    canDoubleJump = true;
                    break;
                case PlayerState.Jumping:
                    myRigidBody.velocity = new Vector3(
                            myRigidBody.velocity.x,
                            0.0f,
                            myRigidBody.velocity.z
                        );
                    myRigidBody.AddForce(Vector3.up * jumpForce);
                    break;
                case PlayerState.DoubleJumping:
                    canDoubleJump = false;
                    myRigidBody.velocity = new Vector3(
                            myRigidBody.velocity.x,
                            0.0f,
                            myRigidBody.velocity.z
                        );
                    myRigidBody.AddForce(Vector3.up * jumpForce);
                    break;
                case PlayerState.Falling:
                    break;
            }
        }
    }

    bool IsOnGround()
    {
        return distanceToGround <= onGroundHeight;
    }

    bool IsFalling()
    {
        return myRigidBody.velocity.y < 0.0f;
    }

    public float GetDistanceToGround()
    {
        return distanceToGround;
    }

    public string GetStateName()
    {
        return myPlayerState.ToString();
    }

    public void ResetToStartingPosition()
    {
        transform.position = startingPosition;
        SetPlayerState(PlayerState.Falling);
    }
}
