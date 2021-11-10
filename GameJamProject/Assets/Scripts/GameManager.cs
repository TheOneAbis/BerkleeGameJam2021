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

    private Animator animatorBass;
    private Animator animatorTreble;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = new Vector3(600, -236, 0);
        playerMode = Mode.Staff;
        moveSpeed = 900;

        // Set animators for staffs for mode transition animations
        animatorBass = bassStaff.GetComponent<Animator>();
        animatorTreble = trebleStaff.GetComponent<Animator>();
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
                        ChangeMode(Mode.Space);
                    else
                        ChangeMode(Mode.Staff);
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
            animatorTreble.Play("StaffShrinkTreble");
            animatorBass.Play("StaffShrinkBass");
        }
        else if (mode == Mode.Staff)
        {
            player.transform.position = new Vector3(player.transform.position.x, playerStaffPosY, player.transform.position.z);
            animatorTreble.Play("StaffExpandTreble");
            animatorBass.Play("StaffExpandBass");
        }
    }
}
