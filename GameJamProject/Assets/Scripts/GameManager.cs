using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Mode
{
    Staff,
    Space
}

public class GameManager : MonoBehaviour
{
    // Public GameObject references
    public GameObject player;
    public GameObject obstacleObj;
    public GameObject trebleStaff;
    public GameObject bassStaff;

    // Determine the game mode
    public static Mode playerMode;
    private float playerStaffPosY; //Store player's staff mode position before they switched to Space mode
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(450, -236, 0);
        playerMode = Mode.Staff;
        moveSpeed = 900;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PauseScreen");
        }

        // Update based on player state
        switch (player.GetComponent<playerUno>().state)
        {
            // Main gameplay
            case State.alive:
                // For now, this control will alter the player mode
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (playerMode == Mode.Staff)
                    {
                        //playerMode = Mode.Space;
                        //playerStaffPosY = player.transform.position.y;
                        //player.transform.position = new Vector3(player.transform.position.x, 0, 0);

                        ChangeMode(Mode.Space);
                    }
                    else
                    {
                        //playerMode = Mode.Staff;
                        //player.transform.position = new Vector3(player.transform.position.x, playerStaffPosY, player.transform.position.z);
                        ChangeMode(Mode.Staff);
                    }
                    //AnimateTransition();
                }
                // Move Camera & player
                Camera.main.transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
                player.transform.Translate(Time.deltaTime * moveSpeed, 0, 0);

                break;
        }
    }

    /// <summary>
    /// Changes player mode to the specified mode
    /// </summary>
    /// <param name="mode">The specified mode to change to</param>
    public void ChangeMode(Mode mode)
    {
        playerMode = mode;
        if (mode == Mode.Space)
        {
            playerStaffPosY = player.transform.position.y;
            player.transform.position = new Vector3(player.transform.position.x, 0, 0);
        }
        else if (mode == Mode.Staff)
        {
            player.transform.position = new Vector3(player.transform.position.x, playerStaffPosY, player.transform.position.z);
        }
        
        AnimateTransition();
    }

    /// <summary>
    /// Animation Transition to Space Mode, shrinking staffs for more space to move around in middle
    /// </summary>
    void AnimateTransition()
    {
        float translateAmt = 90;
        float scaleAmt = 30;
        if (playerMode == Mode.Space) // If transitioning to Space
        {
            trebleStaff.transform.localScale = new Vector3(trebleStaff.transform.localScale.x, trebleStaff.transform.localScale.y - scaleAmt, 1);
            bassStaff.transform.localScale = new Vector3(bassStaff.transform.localScale.x, bassStaff.transform.localScale.y - scaleAmt, 1);

            trebleStaff.transform.Translate(0, translateAmt, 0);
            bassStaff.transform.Translate(0, -translateAmt, 0);
        }
        else // If transitioning to Staff
        {
            trebleStaff.transform.localScale = new Vector3(trebleStaff.transform.localScale.x, trebleStaff.transform.localScale.y + scaleAmt, 1);
            bassStaff.transform.localScale = new Vector3(bassStaff.transform.localScale.x, bassStaff.transform.localScale.y + scaleAmt, 1);

            trebleStaff.transform.Translate(0, -translateAmt, 0);
            bassStaff.transform.Translate(0, translateAmt, 0);
        }
    }
}
