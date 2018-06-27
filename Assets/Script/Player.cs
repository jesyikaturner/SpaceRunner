using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAudioHandler))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    public enum State {LEFT, MIDDLE, RIGHT}
    public State state;

    private PlayerAudioHandler audioHandler;
    public Rigidbody rb;

    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    public bool isAlive = true;
    public bool startGame = false;

    private int coinScore = 0;

    public bool inverse = false;

    //public GameHUD hud;

    public float leftZ = 3f, midZ = 0f, rightZ = -3f;

    public Vector3 currPos;

    public bool isGrounded;
    public float distToGround = 0.0f;

    // Use this for initialization
    private void Start () {
        audioHandler = GetComponent<PlayerAudioHandler>();

        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        state = State.MIDDLE;
    }

    public int GetScore()
    {
        int distScore = (int)Mathf.Round(currPos.x*speed*0.5f / 16);
        int score = distScore + coinScore;
        return score;
    }

	// Update is called once per frame
	private void Update () {
        if (transform.position.y < -5f)
        {
            isAlive = false;
        }

        if (!isAlive)
        {
            audioHandler.ToggleMuteSound();
            startGame = false;
            //hud.GameOver();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coins":
                coinScore += 200;
                audioHandler.PlaySound("coin");
                break;
            case "SpikeTrap":
                isAlive = false;
                break;
            case "SpeedUp":
                coinScore += 400;
                speed += 3;
                break;
            case "SpeedDown":
                coinScore -= 200;
                speed -= 2;
                break;
            case "Inverse":
                inverse = !inverse;
                break;
            default:
                Debug.Log("Unrecognisable tag hit.");
                break;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void HandleMovement()
    {
        if (!isAlive)
            return;

        isGrounded = IsGrounded();

        currPos = transform.position;

        if (isGrounded)
        {
            
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }


            if (Input.GetKeyDown(KeyCode.D)) // right
            {
                if (state == State.MIDDLE)
                {
                    if (!inverse)
                    {
                        state = State.RIGHT;
                    }
                    else
                    {
                        state = State.LEFT;
                    }
                }


                if(state == State.LEFT)
                    if(!inverse)
                        state = State.MIDDLE;
            }

            if (Input.GetKeyDown(KeyCode.A)) // left
            {

                if (state == State.MIDDLE)
                {
                    if (!inverse)
                    {
                        state = State.LEFT;
                    }
                    else
                    {
                        state = State.RIGHT;
                    }
                }

                if(state == State.RIGHT)
                    if(!inverse)
                        state = State.MIDDLE;

            }
            MovePlayer();
        }

        transform.Translate(speed * Time.deltaTime,0,0);
    }

    private void MovePlayer()
    {
        switch (state)
        {
            case State.LEFT:
                if(currPos.z != leftZ)
                    StartCoroutine(MoveToPoint(leftZ,0.1f,1f));
                break;
            case State.MIDDLE:
                if (currPos.z != midZ)
                    StartCoroutine(MoveToPoint(midZ,0.1f,1f));
                break;
            case State.RIGHT:
                if (currPos.z != rightZ)
                    StartCoroutine(MoveToPoint(rightZ,0.1f,1f));
                break;
        }

    }

    private IEnumerator MoveToPoint(float endZ, float speed, float delay)
    {
        Vector3 endPos = Vector3.zero;
        bool arrived = false;

        while (!arrived)
        {
            endPos = new Vector3(transform.position.x, transform.position.y, endZ);
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPos) < 0.2f)
            {
                transform.position = endPos;
                arrived = true;
            }
        }
        yield return new WaitForSeconds(delay);
    }
}
