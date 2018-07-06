using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{

    [SerializeField]
    Vector3 moveSpeedPerSec;

    [SerializeField]
    bool isStop = false;

    [SerializeField]
    GameObject obstaclePrefab;

    [SerializeField]
    float spawnTime;

    [SerializeField]
    float minY;

    [SerializeField]
    float maxY;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(3f);

        while (!isStop)
        {
            //spawn obstacle
            GameObject go = Instantiate(obstaclePrefab);

            //Set parent
            go.transform.parent = transform;

            //Random Y
            float y = UnityEngine.Random.Range(minY, maxY);

            //Set position
            go.transform.position = new Vector3(12, y, 0);

            //Random gap
            //float gap = Random.Range(12f, 20f);
            float halfGap = UnityEngine.Random.Range(6f, 7f);

            go.transform.GetChild(0).localPosition = new Vector3(0f, -halfGap, 0f);
            go.transform.GetChild(1).localPosition = new Vector3(0f, halfGap, 0f);

            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
            return;

        Vector3 moveSpeedPerFrame = Time.deltaTime * moveSpeedPerSec;
        transform.Translate(moveSpeedPerFrame);
    }
    

    public void Stop()
    {
        isStop = true;
    }
}
