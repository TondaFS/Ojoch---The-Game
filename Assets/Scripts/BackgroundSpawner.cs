using UnityEngine;
using System.Collections;
using System;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject hills;
    public GameObject clouds;

    public Vector2 hillSpeed = new Vector2(1, 1); // scrolling speed
    public Vector2 cloudSpeed = new Vector2(1, 1); // scrolling speed
    public Vector2 direction = new Vector2(-1, 0); // moving direction

    private float hillSize;
    private float hillWarpPos;

    private float cloudSize;
    private float cloudWarpPos;

    private GameObject hill1;
    private GameObject hill2;

    private GameObject cloud1;
    private GameObject cloud2;

    void Start()
    {
        hillSize = hills.GetComponent<SpriteRenderer>().bounds.size.x;
        hillWarpPos = -10 - hillSize;

        hill1 = Instantiate(hills, new Vector3(-9, 0, 0), Quaternion.identity) as GameObject;
        hill2 = Instantiate(hills, new Vector3(-9 + hillSize, 0, 0), Quaternion.identity) as GameObject;

        cloudSize = clouds.GetComponent<SpriteRenderer>().bounds.size.x;
        cloudWarpPos = -10 - cloudSize;

        cloud1 = Instantiate(clouds, new Vector3(-9, 0, 0), Quaternion.identity) as GameObject;
        cloud2 = Instantiate(clouds, new Vector3(-9 + cloudSize, 0, 0), Quaternion.identity) as GameObject;
    }

    void Update()
    {
        MoveHills();
        MoveClouds();
        
    }

    private void MoveClouds()
    {
        Vector3 movement = new Vector3(
          hillSpeed.x * direction.x,
          hillSpeed.y * direction.y,
          0);

        movement *= Time.deltaTime;

        hill1.transform.Translate(movement);
        hill2.transform.Translate(movement);

        if (hill1.transform.position.x <= hillWarpPos)
        {
            hill1.transform.Translate(new Vector3(2 * hillSize, 0, 0));
        }

        if (hill2.transform.position.x <= hillWarpPos)
        {
            hill2.transform.Translate(new Vector3(2 * hillSize, 0, 0));
        }
    }

    private void MoveHills()
    {
        Vector3 movement = new Vector3(
          cloudSpeed.x * direction.x,
          cloudSpeed.y * direction.y,
          0);

        movement *= Time.deltaTime;

        cloud1.transform.Translate(movement);
        cloud2.transform.Translate(movement);

        if (cloud1.transform.position.x <= cloudWarpPos)
        {
            cloud1.transform.Translate(new Vector3(2 * cloudSize, 0, 0));
        }

        if (cloud2.transform.position.x <= cloudWarpPos)
        {
            cloud2.transform.Translate(new Vector3(2 * cloudSize, 0, 0));
        }
    }
}
