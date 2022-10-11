using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_RainFollower : MonoBehaviour
{

    public Transform player;
    public Vector3 moveTowardPoint;

    public float speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveTowardPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        moveTowardPoint = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTowardPoint, speed * Time.deltaTime);
    }
}
