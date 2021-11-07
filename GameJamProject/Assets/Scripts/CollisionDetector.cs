using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject obstacles;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check collision between player and each obstacle
        foreach (Transform child in obstacles.transform)
        {
            if (player.GetComponent<playerUno>().state == State.alive && CircleCollide(player, child.gameObject))
            {
                player.GetComponent<playerUno>().StartDeathSequence();
            }
        }
    }

    /// <summary>
    /// Check collision between two game objects
    /// </summary>
    /// <param name="obj1">First object to be tested</param>
    /// <param name="obj2">Second object to be tested</param>
    /// <returns>The collision state between the two game objects</returns>
    public bool CircleCollide(GameObject obj1, GameObject obj2)
    {
        return (obj2.transform.position - obj1.transform.position).sqrMagnitude < 
            Mathf.Pow(obj1.GetComponent<CircleCollider2D>().bounds.extents.x + obj2.GetComponent<CircleCollider2D>().bounds.extents.x, 2);
    }
}
