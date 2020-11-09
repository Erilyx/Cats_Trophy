using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLine : MonoBehaviour
{

    public float lineHeight = 0;
    private float lineRiser = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            //lineRiser += 0.01f;
            transform.position = new Vector3(0, collision.transform.position.y + lineRiser, 0);
        }
    }

}
