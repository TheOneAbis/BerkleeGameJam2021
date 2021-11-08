using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State { alive, dead, transcending };

public class playerUno : MonoBehaviour
{
    public GameObject trebleStaff;
    public GameObject bassStaff;
    public GameObject startRepeat;
    public GameObject EndRepeat;

    [SerializeField] private int staffSpace;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] private float spaceVel;
    Collider2D collider;

    Rigidbody rigidBody;
    public AudioClip deathClip; 
    private AudioSource audioSource;

    public State state = State.alive;
    bool collisionsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        staffSpace = 2;
        spaceVel = 0;

        rigidBody = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = deathClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive){
            StaffMovements();
        }
        if (Debug.isDebugBuild){
            DebugKeys();
        }
    }
    //Debugging
    private void DebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            //toggle collision
            collisionsDisabled = !collisionsDisabled;
        }
    }
    void StaffMovements(){
        switch (GameManager.playerMode)
        {
            case Mode.Staff:
                // Movement Inputs
                GetKey();
                break;

            case Mode.Space:
                // Movement Inputs
                GetKeyAccel();
                break;
        }
    }
    void GetKey(){
        // If player presses UP or Left Click
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0)){
                {
                    if (staffSpace < 7)
                    {
                        if (staffSpace == 3)
                        {
                            transform.Translate(0, 305, 0);
                        }
                        else
                        {
                            transform.Translate(0, 86, 0);
                        }
                        staffSpace++;
                    }
                }
        }
        // If player presses DOWN or Right Click
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(1))
        {
            if (staffSpace > 0)
            {
                if (staffSpace == 4)
                {
                    transform.Translate(0, -305, 0);
                }
                else
                {
                    transform.Translate(0, -86, 0);
                }
                staffSpace--;
            }     
        }
    }

    void GetKeyAccel()
    {
        float maxSpeed = 4.5f;
        int accelAmt = 15;
        // Accel up when key down, slow down when key up
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0))
        {
            spaceVel = spaceVel < maxSpeed ? spaceVel + Time.smoothDeltaTime * accelAmt * 2 : maxSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(1))
        {
            spaceVel = spaceVel > -maxSpeed ? spaceVel - Time.smoothDeltaTime * accelAmt * 2 : -maxSpeed;
        }

        if (spaceVel > 0.01) spaceVel -= Time.smoothDeltaTime * accelAmt;
        else if (spaceVel < -0.01) spaceVel += Time.smoothDeltaTime * accelAmt;
        else spaceVel = 0;

        // Check collisions with top and bottom
        if ((transform.position.y + transform.localScale.y > 
            trebleStaff.transform.position.y - trebleStaff.transform.localScale.y && spaceVel > 0) ||
            (transform.position.y - transform.localScale.y < 
            bassStaff.transform.position.y + bassStaff.transform.localScale.y && spaceVel < 0)) spaceVel = 0;

        // Move player up or down by specified amount
        transform.Translate(0, spaceVel, 0);
    }
    //obstacles
    void OnCollisionEnter2D(Collision2D collision){
        
        if(state != State.alive || collisionsDisabled){ return; }
        
        switch(collision.gameObject.tag){
            case "Win":
                print("win");
                StartSucessSequence();
                break;
            case "StartRepeat":
                print("touched");
                OnTriggerEnter2D(collider);
                break;
            case "Lose":
                StartDeathSequence();
                break;
            default:
                StartDeathSequence();
                break;    
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
       // if startrepeat is touched go to endrepeat
       //for only once
        startRepeat = GameObject.FindWithTag("StartRepeat");
        EndRepeat = GameObject.FindWithTag("EndRepeat");

       for (int i = 0; i < 1; i++){
           if(collider.gameObject.tag == "StartRepeat") {
               collider.transform.position = EndRepeat.transform.position;
               collider.transform.rotation = EndRepeat.transform.rotation;
           }
       }
    }

    //success
    public void StartSucessSequence(){
        print("Win");
        state = State.transcending;
        //audio source will be musicians
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    //death
    public void StartDeathSequence(){
        //return to the beg
        print("lose");
        state = State.dead;
        audioSource.Play();
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); // Disappear from screen

        //audio source will be musicians
        Invoke("LoadCurrentLevel", levelLoadDelay);
        
    }
    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0; // loop back to start
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadCurrentLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        gameObject.SetActive(true);
    }
}
