using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Staff,
    Space
}

public class GameManager : MonoBehaviour
{
    // Public GameObject references
    public GameObject playerObj;
    public GameObject obstacleObj;
    public GameObject camera;
    public GameObject trebleStaff;
    public GameObject bassStaff;

    // Determine the game mode
    public static Mode playerMode;

    private GameObject player;
    private float playerStaffPosY; //Store player's staff mode position before they switched to Space mode
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerObj, new Vector3(350, -236, 0), Quaternion.identity);
        playerMode = Mode.Staff;
        moveSpeed = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // For now, this control will alter the player mode
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (playerMode == Mode.Staff)
            {
                playerMode = Mode.Space;
                playerStaffPosY = player.transform.position.y;
                player.transform.position = new Vector3(player.transform.position.x, 0, 0);
            }
            else
            {
                playerMode = Mode.Staff;
                player.transform.position = new Vector3(player.transform.position.x, playerStaffPosY, player.transform.position.z);
            }
            AnimateTransition();
        }

        // Move Camera & player
        camera.transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
        player.transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
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
