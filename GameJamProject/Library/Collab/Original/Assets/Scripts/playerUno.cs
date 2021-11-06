using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUno : MonoBehaviour
{
    [SerializeField] private int staffSpace;
    private int spaceVel;

    // Start is called before the first frame update
    void Start()
    {
        staffSpace = 2;
        spaceVel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.playerMode)
        {
            case Mode.Staff:
                // If player presses UP or Left Click
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
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
                break;

            case Mode.Space:
                break;
        }
    }
}
