using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomCollider : MonoBehaviour
{
    Player playerScript;
    // Start is called before the first frame update
    private void Awake(){
        playerScript = GetComponentInParent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Ground"){
            playerScript.canJump = true;
            playerScript.myAnim.SetBool("Jump",false);
        }

        if(collision.tag == "AirPlatform"){
            playerScript.canJump = true;
            playerScript.myAnim.SetBool("Jump",false);

            playerScript.transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "AirPlatform"){
            playerScript.transform.parent = null;
        }       
    }
}
