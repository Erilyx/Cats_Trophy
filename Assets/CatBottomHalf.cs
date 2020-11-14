using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBottomHalf : MonoBehaviour
{

    public SurfaceEffector2D surface;
    public Transform thisCatsTop;   //manually assign

    public Quaternion catRotation;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<SurfaceEffector2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        catRotation = this.transform.rotation;
        /*if(catRotation > 1)
        {
            Debug.Log("Cat off balance.");
        }*/


    }
}
