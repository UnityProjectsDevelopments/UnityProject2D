using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float Move;
    float verAxis;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += moviment * Time.deltaTime * Move;
        verAxis = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
}
