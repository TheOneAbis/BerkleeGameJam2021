using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject obstacles;
    public GameObject player;
    public GameObject ModeChangerGroup;
    public GameObject EndGroup; //Group of level end game objects
    public GameObject StartRepeat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Only check collisions is player is alive
        if (player.GetComponent<playerUno>().state == State.alive)
        {
            // Check collision between player and each obstacle
            foreach (Transform child in obstacles.transform)
            {
                if (CircleCollide(player, child.gameObject))
                {
                    player.GetComponent<playerUno>().StartDeathSequence();
                }
            }

            // Check collisions with mode changer blocks
            foreach (Transform child in ModeChangerGroup.transform)
            {
                if (HorizontalCollide(player, child.gameObject))
                {
                    if (child.gameObject.tag.Equals("SpaceBlock") && GameManager.playerMode == Mode.Staff)
                    {
                        GameManager.playerMode = Mode.Space;
                        gameObject.GetComponent<GameManager>().ChangeMode(Mode.Space);
                    } 
                    else if (child.gameObject.tag.Equals("StaffBlock") && GameManager.playerMode == Mode.Space)
                    {
                        GameManager.playerMode = Mode.Staff;
                        gameObject.GetComponent<GameManager>().ChangeMode(Mode.Staff);
                    }
                }
            }

            // Check collisions with Level Success (End) blocks
            foreach (Transform child in EndGroup.transform)
            {
                if (HorizontalCollide(player, child.gameObject))
                {
                    player.GetComponent<playerUno>().StartSucessSequence();
                }
            }

            // Check collisions with Repeat blocks
            foreach (Transform child in StartRepeat.transform)
            {
                if (HorizontalCollide(player, child.gameObject))
                {
                    player.GetComponent<playerUno>().StartRepeatSequence();
                }
            }
        }
    }

    /// <summary>
    /// Check collision between Player and obstacle
    /// </summary>
    /// <param name="obj1">First object to be tested (Circle)</param>
    /// <param name="obj2">Second object to be tested (Box)</param>
    /// <returns>The collision state between the two game objects</returns>
    public bool CircleCollide(GameObject obj1, GameObject obj2)
    {
        return (obj2.transform.position - obj1.transform.position).sqrMagnitude < 
            Mathf.Pow(obj1.GetComponent<CircleCollider2D>().bounds.extents.x + obj2.GetComponent<CircleCollider2D>().bounds.extents.x, 2);
    }

    /// <summary>
    /// Check collision between Player and vertical block
    /// </summary>
    /// <param name="obj1">First object to be tested (Circle)</param>
    /// <param name="obj2">Second object to be tested (Box)</param>
    /// <returns>The collision state between the two game objects</returns>
    public bool HorizontalCollide(GameObject obj1, GameObject obj2)
    {
        float distance = Mathf.Abs(obj2.transform.position.x - obj1.transform.position.x);
        return distance < obj1.GetComponent<CircleCollider2D>().bounds.extents.x + obj2.GetComponent<BoxCollider2D>().bounds.extents.x;
    }

}
