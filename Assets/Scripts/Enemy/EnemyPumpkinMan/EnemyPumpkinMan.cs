using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPumpkinMan : MonoBehaviour
{
    bool isAlive,isIdle,jumpAttack,isJumpUp,slideAttack,isHurt,canBeHurt;

    public int life;
    public float attackDistance,jumpHeight,jumpUpSpeed,jumpDownSpeed,slideSpeed,fallDownSpeed;
    

    GameObject player;
    Animator myAnim;
    Vector3 slideTargetPosition;
    BoxCollider2D myColider;
    SpriteRenderer mySr;
    AudioSource myAudioSource;
    // Start is called before the first frame update

    private void Awake(){

        player = GameObject.Find("Player");
        myAnim = GetComponent<Animator>();
        myColider = GetComponent<BoxCollider2D>();
        mySr = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();

        isAlive = true;
        isIdle = true;
        jumpAttack = false;
        isJumpUp = true;
        slideAttack = false;
        isHurt = false;
        canBeHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            if(isIdle){
                LookAtPlayer();
                if(Vector3.Distance(player.transform.position,transform.position) <= attackDistance){
                    //slideAttack
                    isIdle = false;
                    StartCoroutine("IdleToSlideAttack");
                }else{
                    //jumpAttack
                    isIdle = false;
                    StartCoroutine("IdleToJumpAttack");
                }
            }else if(jumpAttack){
                LookAtPlayer();
                if(isJumpUp){
                    Vector3 myTarget = new Vector3(player.transform.position.x,jumpHeight,transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position,myTarget,jumpUpSpeed * Time.deltaTime);
                    myAnim.SetBool("JumpUp",true);
                }else{
                    myAnim.SetBool("JumpUp",false);
                    myAnim.SetBool("JumpDown",true);
                    Vector3 myTarget = new Vector3(transform.position.x,-2.84f,transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position,myTarget,jumpDownSpeed * Time.deltaTime);
                }

                if(transform.position.y == jumpHeight){
                    isJumpUp = false;
                }else if(transform.position.y == -2.84f){
                    jumpAttack = false;
                    StartCoroutine("JumpDownToIdle");
                }
            }else if(slideAttack){
                myAnim.SetBool("Slide",true);
                transform.position = Vector3.MoveTowards(transform.position, slideTargetPosition,slideSpeed*Time.deltaTime);

                if(transform.position == slideTargetPosition){
                    myColider.offset= new Vector2(-0.1341941f,-0.1661543f);
                    myColider.size= new Vector2(1.187618f,1.976355f);
                    myAnim.SetBool("Slide",false);
                    slideAttack = false;
                    isIdle = true;
                }
            }else if(isHurt){
                Vector3 myTargetPosition = new Vector3(transform.position.x,-2.84f,transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position,myTargetPosition,fallDownSpeed*Time.deltaTime);
            }
        }else{
            Vector3 myTargetPosition = new Vector3(transform.position.x,-2.84f,transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position,myTargetPosition,fallDownSpeed*Time.deltaTime);            
        }
    }

    void LookAtPlayer(){
        if(player.transform.position.x <= transform.position.x){
            transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        }else{
            transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        }
    }

    IEnumerator IdleToSlideAttack(){
        yield return new WaitForSeconds(1.0f);
        myColider.offset= new Vector2(-0.1341941f,-0.4046283f);
        myColider.size= new Vector2(1.187618f,1.499407f);
        slideTargetPosition = new Vector3(player.transform.position.x,transform.position.y,transform.position.z);
        LookAtPlayer();
        slideAttack = true;
    }

    IEnumerator IdleToJumpAttack(){
        yield return new WaitForSeconds(1.0f);
        jumpAttack = true;
    }

    IEnumerator JumpDownToIdle(){
        yield return new WaitForSeconds(0.1f);
        isIdle = true;
        isJumpUp = true;
        myAnim.SetBool("JumpUp",false);
        myAnim.SetBool("JumpDown",false);
    }

    IEnumerator SetAnimHurtToFalse(){
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("Hurt",false);
        myAnim.SetBool("JumpUp",false);
        myAnim.SetBool("JumpDown",false);
        myAnim.SetBool("Slide",false);
        isHurt = false;
        isIdle = true;
        mySr.material.color = new Color(1.0f,1.0f,1.0f,0.5f);
        yield return new WaitForSeconds(2.0f);
        canBeHurt = true;
        mySr.material.color = new Color(1.0f,1.0f,1.0f,1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "PlayerAttack"){
            if(canBeHurt){
                myAudioSource.PlayOneShot(myAudioSource.clip);
                life--;
                if(life >= 1){
                    isIdle = false;
                    jumpAttack = false;
                    slideAttack = false;
                    isHurt = true;
                    StopCoroutine("IdleToSlideAttack");
                    StopCoroutine("IdleToJumpAttack");
                    StopCoroutine("JumpDownToIdle");
                    myAnim.SetBool("Hurt",true);
                    StartCoroutine("SetAnimHurtToFalse");
                }else{
                    isAlive = false;
                    myColider.enabled = false;
                    StopAllCoroutines();
                    myAnim.SetBool("Dead",true);
                    Time.timeScale = 0.5f;
                    StartCoroutine(AfterDie());
                }

                canBeHurt = false;
            }
        }
    }

    IEnumerator AfterDie(){
        yield return new WaitForSecondsRealtime(3f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
}


//-0.1341941 -0.4046283
//1.187618 1.499407