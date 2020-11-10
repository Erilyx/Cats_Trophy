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



    public void SetLineHeight(float newLineHeight)
    {
        transform.position = new Vector3(0, newLineHeight, 0);
    }

}
