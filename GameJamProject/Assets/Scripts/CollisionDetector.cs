using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject obstacles;
    public GameObject player;
    public GameObject ModeChangerGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Only chekck collisions is player is alive
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
                if (ModeChangeCollide(player, child.gameObject))
                {
                    if (child.gameObject.tag.Equals("SpaceBlock") && GameManager.playerMode == Mode.Staff)
                        gameObject.GetComponent<GameManager>().ChangeMode(Mode.Space);
                    else if (child.gameObject.tag.Equals("StaffBlock") && GameManager.playerMode == Mode.Space)
                        gameObject.GetComponent<GameManager>().ChangeMode(Mode.Staff);
                }
            }
        }
    }

    /// <summary>
    /// Check collision between Player and Mode Changer block
    /// </summary>
    /// <param name="obj1">First object to be tested (Circle)</param>
    /// <param name="obj2">Second object to be tested (Box)</param>
    /// <returns>The collision state between the two game objects</returns>
    public bool CircleCollide(GameObject obj1, GameObject obj2)
    {
        return (obj2.transform.position - obj1.transform.position).sqrMagnitude < 
            Mathf.Pow(obj1.GetComponent<CircleCollider2D>().bounds.extents.x + obj2.GetComponent<CircleCollider2D>().bounds.extents.x, 2);
    }
    public bool ModeChangeCollide(GameObject obj1, GameObject obj2)
    {
        return obj2.transform.position.x - obj1.transform.position.x <
            obj1.GetComponent<CircleCollider2D>().bounds.extents.x + obj2.GetComponent<BoxCollider2D>().bounds.extents.x;
    }
}
