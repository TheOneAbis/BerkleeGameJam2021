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

    // Determine the game mode
    public static Mode playerMode;

    private GameObject player;
    private Vector3 playerStaffPos; //Store player's staff mode position before they switched to Space mode

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerObj, new Vector3(250, -236, 0), Quaternion.identity);
        playerMode = Mode.Staff;
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
                playerStaffPos = player.transform.position;
                player.transform.position = new Vector3(250, 0, 0);
            }
            else
            {
                playerMode = Mode.Staff;
                player.transform.position = playerStaffPos;
            }
        }
    }
}
