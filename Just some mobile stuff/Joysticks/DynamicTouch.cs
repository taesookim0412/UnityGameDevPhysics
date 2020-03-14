using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Rigidbody player;
    public float shootForce = 10000f;
    private Transform playerPos;
    private Vector3 oldMousePosition;
    private float time;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerPos = player.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator countDown()
    {
        while (time > 0f)
        {
            time -= .05f;
            Debug.Log(time);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (time <= 0f) {
            time = .3f;
            StartCoroutine(countDown());
        }
        oldMousePosition = Input.mousePosition;
    }
    


    public void OnPointerUp(PointerEventData eventData)
    {

        if (oldMousePosition == Input.mousePosition || time > 0f)
        { 
        Vector3 pos = playerPos.position;
        pos.z = player.transform.forward.z;
        GameObject shotProj = Instantiate(projectile, pos, Quaternion.identity);
        shotProj.GetComponent<Rigidbody>().position = player.position;
        shotProj.GetComponent<Rigidbody>().AddForce(player.transform.forward * shootForce);
        }


    }
}
