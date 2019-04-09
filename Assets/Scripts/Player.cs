using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using TMPro;

public class Player : Photon.PunBehaviour
{
    public TextMeshProUGUI userText;
    public TextMeshProUGUI healthText;
    public float speed; 
    public Rigidbody rg;
    public string username;
    public Vector3 camPosOffset;
    private float x;
    private float z;
    private float _health;
    private float _maxHealth = 100;
    private float _minHealth = 0;
    // Start is called before the first frame update
    void Start(){

        rg = GetComponent<Rigidbody>();
        _health = _maxHealth;

        if (photonView.isMine){
            userText.text = username;

        }else{
            
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Hurt"){

            if (photonView.isMine){
                
                photonView.RPC("Damage", PhotonTargets.All);
            }
        }
    }

    [PunRPC]
    private void Damage(){
        _health -= 10;
    }

    // Update is called once per frame
    void Update(){

        if (photonView.isMine){

            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");

            Vector3 camPos = Camera.main.transform.position;
            Vector3 qPos = transform.position;
            Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(qPos.x , qPos.y, qPos.z) + camPosOffset, 2f * Time.deltaTime);

            if(_health > _maxHealth){
                _health = _maxHealth;
            } 
            if(_health < _minHealth) {
                _health = _minHealth;
            }
        }else{
            
        }
    }

    private void FixedUpdate() {
        
        healthText.text = _health.ToString();
        if (photonView.isMine){

            rg.AddForce(new Vector3(x,0,z) * speed);
        }else{
            
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.isWriting){
            
            //Mine component
            stream.SendNext(_health);
            stream.SendNext(username);
        }else if(stream.isReading){

            //Their component
            _health = (float) stream.ReceiveNext();
            username = (string) stream.ReceiveNext();
            userText.text = username;
        }
    }
}
