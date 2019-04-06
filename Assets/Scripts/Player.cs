using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : Photon.PunBehaviour
{
    public Rigidbody rg;
    public float speed; 
    private float x;
    private float z;
    // Start is called before the first frame update
    void Start(){

        rg = GetComponent<Rigidbody>();

        if (photonView.isMine){
            
        }else{
            
        }
    }

    // Update is called once per frame
    void Update(){

        if (photonView.isMine){

            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        }else{
            
        }
    }

    private void FixedUpdate() {
        
        if (photonView.isMine){

            rg.AddForce(new Vector3(x,0,z) * speed);
        }else{
            
        }
    }
}
