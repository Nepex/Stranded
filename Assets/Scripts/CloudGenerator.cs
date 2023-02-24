using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] clouds;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject endpoint;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        PreWarm();
        Invoke(nameof(AttemptSpawn), spawnInterval);
    }


    void AttemptSpawn()
    {
        // check if we should spawn cloud
        SpawnCloud(startPos);
        Invoke(nameof(AttemptSpawn), spawnInterval);
    }

    void SpawnCloud(Vector3 startPos)
    {
        // select a random cloud to spawn from the array, and create a new GameObject
        int randomIndex = UnityEngine.Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        // randomize altitude
        float altitude = UnityEngine.Random.Range(startPos.y - 1f, startPos.y + 1f);
        cloud.transform.position = new Vector3(startPos.x, altitude, startPos.z);

        // randomize size
        float scale = UnityEngine.Random.Range(0.8f, 1.2f);
        cloud.transform.transform.localScale = new Vector2(scale, scale);

        // randomize speed
        //float speed = UnityEngine.Random.Range(0.5f, 1.5f);
        float speed = .5f;

        // start moving the cloud
        cloud.GetComponent<Cloud>().StartFloating(speed, endpoint.transform.position.x);
    }

    // make sure clouds are already spawned when game starts
    void PreWarm()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 3);
            SpawnCloud(spawnPos);
        }    
    }
}
