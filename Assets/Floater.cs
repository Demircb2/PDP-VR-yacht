using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBEfSub;
    public float displacementAmt;
    public int floaters;
    public float waterDrag;
    public float waterAngularDrag;
    public WaterSurface water;
    WaterSearchParameters waterSearchParameters;
    WaterSearchResult waterSearchResult;

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaters, transform.position , ForceMode.Acceleration);
        
        waterSearchParameters.startPosition = transform.position;

        water.FindWaterSurfaceHeight(waterSearchParameters, out waterSearchResult);
    
        if (transform.position.y < waterSearchResult.height)
        {
            float displacementMulti =  Mathf.Clamp01(waterSearchResult.height-transform.position.y / depthBEfSub * displacementAmt);
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration); 
            rb.AddForce(displacementMulti*-rb.velocity*waterDrag*Time.fixedDeltaTime,ForceMode.VelocityChange);
            rb.AddTorque(displacementMulti*-rb.angularVelocity*waterAngularDrag*Time.fixedDeltaTime, ForceMode.VelocityChange);
        
        }

    
    }



}
