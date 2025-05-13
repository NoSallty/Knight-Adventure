using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float speed = 200f;
    public float activateDistance = 500f;
    public float nextWayPointDistance = 3f;

    public Transform enemy;

    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position) < activateDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        if (enemy.name.Equals("Enemy_Skeleton(Clone)") || enemy.name.Equals("Enemy_Shady(Clone)"))
            rb.velocity = new Vector2(force.x, rb.velocity.y);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }

        //if (rb.velocity.x >= 0.01f)
        //{
        //    enemy.transform.localScale = new Vector3(1, 1, 1);
        //}
        //else if (rb.velocity.x <= -0.01f)
        //{
        //    enemy.transform.localScale = new Vector3(1, 1, 1);
        //}
    }
}
