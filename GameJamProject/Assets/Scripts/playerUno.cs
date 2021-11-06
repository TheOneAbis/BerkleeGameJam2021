using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerUno : MonoBehaviour
{
    [SerializeField] private int staffSpace;
    [SerializeField] float levelLoadDelay = 2f;
    private int spaceVel;

    enum State{alive, dead, transcending};
    State state = State.alive;
    bool collisionsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        staffSpace = 2;
        spaceVel = 0;
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
            //collisionsDisabled = !collisionsDisabled;
        }
    }
    void StaffMovements(){
        switch (GameManager.playerMode)
        {
            case Mode.Staff:
                GetKey();
                break;

            case Mode.Space:
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
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(1)){
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
    }
    //obstacles
    void OnCollisionEnter(Collision collision){
        if(state != State.alive || collisionsDisabled){ return; }
        
        switch(collision.gameObject.tag){
            case "Win":
                StartSucessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    //success
    void StartSucessSequence(){
        state = State.transcending;
        //audio source will be musicians
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    //death
    void StartDeathSequence(){
        //return to the beg
        state = State.dead;
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
    }
}
