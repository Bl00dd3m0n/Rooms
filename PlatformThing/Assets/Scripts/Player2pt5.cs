using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2pt5 : MonoBehaviour
{
    //Referenced https://answers.unity.com/questions/707526/check-if-2d-character-is-grounded.html for groundcheck if you have a better way of doing it feel free to change
    //private for now can make public later
    private int startinghealth;
    public GameObject gameManager;
    GameManager gm;
    public float moveSpeed;
    public float jumpHeight;
    Rigidbody rgbd;
    bool isGrounded;
    internal PlayerAttributes pa;
    public void ResetPlayer()
    {
        pa.health = startinghealth;
        //pa.Collectables = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Not sure if we decided on hearts or health bar(Can be changed)
        startinghealth = 3;
        gm = gameManager.GetComponent<GameManager>();
        rgbd = this.gameObject.GetComponent<Rigidbody>();
        pa = new PlayerAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //For Physics movement
    void FixedUpdate()
    {
        CheckGrounded();
        PlayerJump();
        MovePlayer();
    }
    //If Player presses Jump sees if it can jump then jumps
    void PlayerJump()
    {
        if (isGrounded)
        {
            float Jump = Input.GetAxis("Jump");
            rgbd.AddForce(new Vector3(0, 0, -Jump * jumpHeight), ForceMode.Impulse);
            isGrounded = false;
        }
    }
    //Raycasts downward to see if the player is on the ground if so it sets isGrounded to true
    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), -Vector2.up, 0.1f);
        if(hit.collider != null && hit.collider.tag != "Player")
        {
            Debug.Log(hit.collider);
            isGrounded = true;
        } 
    }
    void MovePlayer()
    {
        float HorizontalMoveMent = Input.GetAxis("Horizontal");
        float VerticalMoveMent = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(HorizontalMoveMent * moveSpeed, 0, 0);
        Vector3 verticalmovement = new Vector3(0, VerticalMoveMent * moveSpeed, 0);
        rgbd.velocity = new Vector3(Time.smoothDeltaTime * HorizontalMoveMent * moveSpeed, Time.smoothDeltaTime * VerticalMoveMent * moveSpeed, rgbd.velocity.y+0.5f);//https://answers.unity.com/questions/1315922/using-rbvelocity-causes-low-gravity.html Still not sure this is the best movement format but it is the most function I've found so far may change later
        
    }
    //
    private void PlayerTakeDamage()
    {
        if (pa.health > 0)//If player has health take damage
        {
            pa.health -= 1;
            Debug.Log(pa.health);
        } else //If not Game is lost
        {
            gm.GameOver("You lose");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard") 
        {
            //Seperate it from the ontrigger method
            PlayerTakeDamage();
            //Destroy hazardous objects when they hit the player.
            Destroy(collision.gameObject);
        } else
        {
            Debug.Log(collision); 
        }
    }
}
