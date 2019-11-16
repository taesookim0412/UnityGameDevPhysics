using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rig;
    private AudioSource audioSrc;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            tryJump();
        }
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;
        
        Vector3 facingDir = new Vector3(xInput, 0, zInput);
        if (facingDir.magnitude > 0)
        { 
            transform.forward = facingDir;
        }
    }
    
    void tryJump()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);

        bool cast1 = Physics.Raycast(ray1, 0.7f);
        bool cast2 = Physics.Raycast(ray2, 0.7f);
        bool cast3 = Physics.Raycast(ray3, 0.7f);
        bool cast4 = Physics.Raycast(ray4, 0.7f);

        if (cast1 || cast2 || cast3 || cast4)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.addScore(1);
            Destroy(other.gameObject);
            audioSrc.Play();
            //audioSrc.clip = var;
            //audioSrc.PlayOneShot(var);
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.levelEnd();
        }
    }
}
