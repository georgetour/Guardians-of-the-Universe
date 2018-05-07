using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;

    //Width and height of the formation
    public float width = 5f;
    public float height = 5f;
    
    //Max left,right and speed of the formation
    private float xmin;
    private float xmax;
    private float ymin;
    private float ymax;
    public float speedX = 3f;
    private float speedY = 2f;
    public float spawnDelay = 0.8f;

    private bool movingToBottom = true;

    public Rigidbody2D rb;

    private GameObject playerShip;

    Vector3 distance;

    // Use this for initialization
    void Start () {

        playerShip = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        RestrictPosition();
        SpawnUntilFull();
        Debug.Log(speedY);


    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
        EnemiesMovement();

        if (AllMembersDead())
        {
            SpawnUntilFull();
            Destroy(gameObject);
        }
       
    }

    


    void RestrictPosition()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1f, distance));
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        xmin = leftMost.x + (width/2);
        xmax = rightMost.x - (width/2);
        ymin = 0.5f;
        ymax = 2.3f;
        
        
    }


    //Move enemies right and left by reversing their direction when hit limit
    void EnemiesMovement()
    {

        distance.x = transform.position.x -playerShip.transform.position.x;
        distance.y = transform.position.y - playerShip.transform.position.y;

        //rb.velocity.x = playerShip.transform.position;

        bool moving = false;

        //if (distance.x > 0 && distance.x <5)
        //{
        //    speedX *= +1;
        //}
        //else if(distance.x < 2)
        //{
        //    speedX *= -1;
        //}

        if (distance.y > 10f && moving == false)
        {
            moving = true;
            speedY *= -1;
        }

        else if (distance.y < 3f && moving == true)
        {
            moving = false;
            speedY *= +1;
        }



        rb.velocity = new Vector3(0, speedY);
        Debug.Log("sppedy"+speedY + "dist"+distance.y);
        //transform.position += new Vector3(speedX *Time.deltaTime,0,0);

    }
   


    //Check if all enemies have died
    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
                return false;
        }
        return true;
    }


    //Return next free position so we spawn enemies in there
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
                return childPositionGameObject;
        }
        return null;

    }


    //Spawn new enemies
    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            //Spawn new enemy for every child EnemySpwane has
            //And keep hierarchy in editor and the Instantiation wil be a GameObject
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;

            //Make parent wherever this script is attached to which is the Position
            enemy.transform.parent = child;
        }
    }

    //Spawn enemies in free position if we have a free position
    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
            Invoke("SpawnUntilFull", spawnDelay);
        
    }
}
