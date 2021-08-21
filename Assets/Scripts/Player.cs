using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject character;
    public float movementSpeed = 5;
    public float jumpForce = 7;
    public Collider2D groundChecker;
    public Collider2D leftWallChecker;
    public Collider2D rightWallChecker;
    public Rigidbody2D bullet;
    public float bulletSpeed = 15;
    public GameObject bulletSpawner;
    
    private Rigidbody2D rb;
    private int direction;
    private bool jump;
    private bool isGrounded;
    private bool isOnLeftWall;
    private bool isOnRightWall;
    private int nEnemies;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = false;
        isOnLeftWall = false;
        isOnRightWall = false;
        nEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update() {
        bool jumpInput = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space);
        if(jumpInput && (isGrounded || isOnLeftWall || isOnRightWall))
            jump = true;
        if(Input.GetMouseButtonDown(0))
            Shoot();

        if(transform.position.x < -9) {
            transform.position = new Vector3(9, transform.position.y + 0.3f, transform.position.z);
        }
        if(transform.position.x > 9) {
            transform.position = new Vector3(-9, transform.position.y + 0.3f, transform.position.z);
        }
    }

    void FixedUpdate() {
        CheckIfGrounded();
        CheckIfOnLeftWall();
        CheckIfOnRightWall();
        Move();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Finish") {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == nEnemies) {
                Debug.Log("Win");
            }else {
                Debug.Log("Loose");
            }
        }
    }

    private void Move() {
        direction = 0;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction -= 1;
            if(isOnLeftWall)
                direction = 0;
            character.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction += 1;
            if(isOnRightWall)
                direction = 0;
            character.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        rb.velocity = new Vector2(movementSpeed * direction, rb.velocity.y);
    }

    private void CheckIfGrounded() {
        if(groundChecker.IsTouchingLayers(LayerMask.GetMask("Ground")) || groundChecker.IsTouchingLayers(LayerMask.GetMask("Wall")))
            isGrounded = true;
        else
            isGrounded = false;
    }
    
    private void CheckIfOnLeftWall() {
        if(leftWallChecker.IsTouchingLayers(LayerMask.GetMask("Wall")))
            isOnLeftWall = true;
        else
            isOnLeftWall = false;
    }
    
    private void CheckIfOnRightWall() {
        if(rightWallChecker.IsTouchingLayers(LayerMask.GetMask("Wall")))
            isOnRightWall = true;
        else
            isOnRightWall = false;
    }

    private void Jump() {
        if(jump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            jump = false;
        }
    }

    private void Shoot() {
        Rigidbody2D clone = Instantiate(bullet, bulletSpawner.transform.position, Quaternion.identity);

        if(character.transform.eulerAngles.y == 0)
            clone.velocity = Vector2.right * bulletSpeed;
        else
            clone.velocity = Vector2.left * bulletSpeed;
    }
}
