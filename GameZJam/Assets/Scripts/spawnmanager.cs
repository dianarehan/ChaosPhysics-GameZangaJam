/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float repeatrate;
    [SerializeField] GameObject moving_platformprefab;
    [SerializeField] GameObject projectilesprefab;
    [SerializeField] GameObject applesprefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnplatforms", time, repeatrate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawnplatforms()
    {
        float xposlimit = 3.44f; ;
        float xposlimitt = -3.26f;
        float randomxpos = (Random.Range(xposlimit, xposlimitt));
        Instantiate(moving_platformprefab, new Vector2(14.63f, randomxpos), transform.rotation);
    }
    void spawnprojectiles()
    {
        float xposlimit = 3.44f;
        float xposlimitt = 3.26f;
        float randomxpos = (Random.Range(xposlimit, xposlimitt));
        Instantiate(projectilesprefab, new Vector2(14.63f, randomxpos), transform.rotation);
    }

    void spawnapples()
    {
        float xposlimit = 3.44f;
        float xposlimitt = 3.26f;
        float randomxpos = (Random.Range(xposlimit, xposlimitt));
        Instantiate(applesprefab, new Vector2(14.63f, randomxpos), transform.rotation);
    }
}
    */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float repeatrate;
    [SerializeField] GameObject moving_platformprefab;
    [SerializeField] GameObject projectilesprefab;
    [SerializeField] GameObject applesprefab;
    [SerializeField] GameObject trapprefab;
    [SerializeField] int platformCountForApple = 5; // Number of platforms to spawn between each apple

    [SerializeField] int platformCountForTrap = 2; // Number of platforms to spawn between each trap
    [SerializeField] int frameCountForCollider = 20; // Number of frames to enable/disable the capsule collider

    private int platformCount = 0; // Counter for spawned platforms
    private int frameCounter = 0; // Counter for frames
    [SerializeField] int platformsToSpawn = 5; // Number of platforms to spawn before spawning an apple or a trap
    private int platformsSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnplatforms", time, repeatrate);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void spawnplatforms()
    {
        float xposlimit = 3.44f;
        float xposlimitt = -3.26f;
        float randomxpos = Random.Range(xposlimit, xposlimitt);
        GameObject platform = Instantiate(moving_platformprefab, new Vector2(14.63f, randomxpos), Quaternion.identity);

        platformCount++; // Increase the platform counter

        // Check if the platform count reaches the desired number for spawning an apple
        if (platformCount % platformCountForApple == 0)
        {
            // Spawn an apple on the platform
            GameObject apple = Instantiate(applesprefab, platform.transform.position + new Vector3(0f, 1.0f, 0f), Quaternion.identity);
            apple.transform.SetParent(platform.transform);
        }
        if (platformCount % platformCountForTrap == 0)
        {
            // Spawn a trap on the platform
            GameObject trap = Instantiate(trapprefab, platform.transform.position + new Vector3(Random.Range(-2.0f, 2f), 1.0f, 0f), Quaternion.identity);
            trap.transform.SetParent(platform.transform);
        }

        // Enable/disable the capsule collider every frameCountForCollider frames
        frameCounter++;
        if (frameCounter % frameCountForCollider == 0)
        {
            Collider2D capsuleCollider = platform.GetComponentInChildren<CapsuleCollider2D>();
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = !capsuleCollider.enabled;
            }
        }
    
    }
    /*void spawnplatforms()
    {
        float xposlimit = 3.44f;
        float xposlimitt = -3.26f;
        float randomxpos = Random.Range(xposlimit, xposlimitt);
        GameObject platform = Instantiate(moving_platformprefab, new Vector2(14.63f, randomxpos), Quaternion.identity);

        platformsSpawned++; // Increase the spawned platform counter

        bool spawnApple = platformsSpawned % platformsToSpawn == 0 && platformsSpawned % platformCountForTrap != 0;
        bool spawnTrap = platformsSpawned % platformCountForTrap == 0 && platformsSpawned % platformsToSpawn != 0;

        if (spawnApple)
        {
            // Spawn an apple on the platform
            GameObject apple = Instantiate(applesprefab, platform.transform.position + new Vector3(0f, 1.0f, 0f), Quaternion.identity);
            apple.transform.SetParent(platform.transform);
        }

        if (spawnTrap)
        {
            // Spawn a trap on the platform
            GameObject trap = Instantiate(trapprefab, platform.transform.position + new Vector3(Random.Range(-2.0f, 2f), 1.0f, 0f), Quaternion.identity);
            trap.transform.SetParent(platform.transform);
        }

        if (!spawnApple && !spawnTrap)
        {
            // Neither apple nor trap need to be spawned, so destroy the empty platform
            Destroy(platform);
        }

        // Enable/disable the capsule collider every frameCountForCollider frames
        frameCounter++;
        if (frameCounter % frameCountForCollider == 0)
        {
            Collider2D capsuleCollider = platform.GetComponentInChildren<CapsuleCollider2D>();
            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = !capsuleCollider.enabled;
            }
        }
    }*/



}

