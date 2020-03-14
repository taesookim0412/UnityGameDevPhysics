using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rig;
    private Rigidbody enemyRig;
    public FixedJoystick fixedJoystick;
    public DynamicJoystick dynamicJoystick;
    public float moveSpeed;
    private float health = 100;
    // Start is called before the first frame update

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(faceEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        rotatePlayer();
    }

    void Move()
    {
        // Testing: Movements with WASD
        /*        float xInput = Input.GetAxis("Horizontal");
                float zInput = Input.GetAxis("Vertical");

                Vector3 dir = new Vector3(xInput, rig.velocity.y, zInput) * moveSpeed;
                rig.velocity = dir;*/
        //

        // Move With Joystick
        if (fixedJoystick.Horizontal >= .2f || fixedJoystick.Horizontal <= -.2f || fixedJoystick.Vertical >= .2f || fixedJoystick.Vertical <= -.2f)
        {
            Vector3 newVelocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
            if (fixedJoystick.Horizontal >= .2f || fixedJoystick.Horizontal <= -.2f)
            {
                newVelocity.x = fixedJoystick.Horizontal * moveSpeed;
            }
            if (fixedJoystick.Vertical >= .2f || fixedJoystick.Vertical <= -.2f)
            {
                newVelocity.z = fixedJoystick.Vertical * moveSpeed;
            }
            rig.velocity = newVelocity;
        }
        else
        {
            rig.velocity = new Vector3(0f, 0f, 0f);
        }
        
    }

    void rotatePlayer()
    {
            if (dynamicJoystick.Horizontal >= .2f || dynamicJoystick.Horizontal <= -.2f || dynamicJoystick.Vertical >= .2f || dynamicJoystick.Vertical <= -.2f)
            {
            float angle = Mathf.Atan2(dynamicJoystick.Vertical, dynamicJoystick.Horizontal) * Mathf.Rad2Deg;
                Vector3 currentRotation = new Vector3(0, 90f - angle, 0);
                rig.transform.eulerAngles = currentRotation;
            }
    }

    IEnumerator faceEnemy()
    {
        while (true)
        {
            if (enemyRig != null)
            {
                Vector3 enemyPos = enemyRig.position;
                normalizeY(ref enemyPos);
                Vector3 relativePos = enemyPos - transform.position;
                transform.rotation = Quaternion.LookRotation(relativePos, transform.up);
            }
            yield return null;
        }
    }

    private void normalizeY(ref Vector3 enemyPos)
    {
        enemyPos.y = transform.position.y;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GroundIndicator"))
        {
            if (GameManager.instance.onCollisionGroundEffect())
            {
                health -= 10;
                HealthManager.instance.updateHealthBar(health);
            }
        }
    }
}
