using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaleZombie : MonoBehaviour
{
    public Vector3 targetPosition;
    public float mySpeed;
    public GameObject attackCollider;
    public int enemyLife;

    protected Animator myAnim;
    protected Vector3 originPosition, turnPoint;

    protected bool isFirstTimeIdle,isAfterBattleCheck,isAlive;

    protected GameObject myPlayer;
    protected BoxCollider2D myColider;
    protected SpriteRenderer mySr;
    [SerializeField]
    protected AudioClip[] myAudioClip;
    protected AudioSource myAudioSource;

    protected virtual void Awake(){
        myAnim = GetComponent<Animator>();
        myColider = GetComponent<BoxCollider2D>();
        mySr = GetComponent<SpriteRenderer>();
        myPlayer = GameObject.Find("Player");
        myAudioSource = GetComponent<AudioSource>();
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        isFirstTimeIdle = true;
        isAfterBattleCheck = false;
        isAlive = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndAttack();
    }

    protected virtual void MoveAndAttack(){
        if(isAlive){
            if(Vector3.Distance(myPlayer.transform.position, transform.position) < 1.3f){

                if(myPlayer.transform.position.x <= transform.position.x){
                    transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
                }else{
                    transform.localScale = new Vector3(1.0f,1.0f,1.0f);
                }

                if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attact") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("AttactWait")){
                    return;
                }
                myAudioSource.PlayOneShot(myAudioClip[1]);
                myAnim.SetTrigger("Attack");
                isAfterBattleCheck = true;
                return;
            }else{
                if(isAfterBattleCheck){
                    if(turnPoint == targetPosition){
                        StartCoroutine(TurnRight(false));
                    }else if(turnPoint == originPosition){
                        StartCoroutine(TurnRight(true));
                    }
                    isAfterBattleCheck = false;
                }           
            }

            if(transform.position.x == targetPosition.x){
                myAnim.SetTrigger("Idle");
                turnPoint = originPosition;
                StartCoroutine(TurnRight(true));
                isFirstTimeIdle = false;
            }else if(transform.position.x == originPosition.x){
                if(!isFirstTimeIdle){
                    myAnim.SetTrigger("Idle");
                }
                turnPoint = targetPosition;
                StartCoroutine(TurnRight(false));
            }

            if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Walk")){
                transform.position = Vector3.MoveTowards(transform.position, turnPoint,mySpeed*Time.deltaTime);
            }
        }        
    }

    protected IEnumerator TurnRight(bool turnRight){
        yield return new WaitForSeconds(2.0f);
        if(turnRight){
            transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        }else if(!turnRight){
            transform.localScale = new Vector3(-1.0f,1.0f,1.0f);
        }
    }

    public void SetAttackColliderOn(){
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff(){
        attackCollider.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "PlayerAttack"){
            myAudioSource.PlayOneShot(myAudioClip[0]);
            enemyLife--;
            if(enemyLife >= 1){
                myAnim.SetTrigger("Hurt");
            }else if(enemyLife < 1){
                isAlive = false;
                myColider.enabled = false;
                myAnim.SetTrigger("Dead");
                StartCoroutine("AfterDie");
            }
        }
    }

    IEnumerator AfterDie(){
        yield return new WaitForSeconds(0.6f);
        mySr.material.color = new Color(1.0f,1.0f,1.0f,0.5f);
        yield return new WaitForSeconds(0.6f);
        mySr.material.color = new Color(1.0f,1.0f,1.0f,0.2f);
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
