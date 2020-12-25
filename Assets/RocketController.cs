using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    Rigidbody2D RB;
    public LayerMask Mask;
    public Vector2 Size;
    bool IsHit;
    public float TorqueForce;
    public float RocketForce;
    public Text VelocityCounter;

    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocityCounter.text = RB.velocity.y.ToString();
        if (Input.GetKey(KeyCode.A))
        {
            RB.AddTorque(TorqueForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RB.AddTorque(-TorqueForce *Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            RB.AddForce(transform.up * RocketForce);
        }
        CheckLanding();

    }
    void CheckLanding()
    {

        Collider2D ColliderCheck = Physics2D.OverlapBox(transform.position, Size, 0, Mask);
        if (ColliderCheck.gameObject.tag == "Ground" && !IsHit)
        {
            IsHit = true;
            if (RB.velocity.y <= -2)
            {
                Debug.Log("Explosion");
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Land");
            }
            Debug.Log(RB.velocity.y);
        }
        if((ColliderCheck.gameObject.tag == "Border" && !IsHit))
        {
            IsHit = true;
            gameObject.SetActive(false);
            Debug.Log("Got out of map");
        }
            

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Size);


    }





}