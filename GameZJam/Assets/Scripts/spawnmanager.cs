using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float repeatrate;
    [SerializeField] GameObject moving_platformprefab;
    [SerializeField] GameObject projectilesprefab;
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
        float xposlimit = 0.46f;
        float xposlimitt = 1.64f;
        float randomxpos = (Random.Range(xposlimit, xposlimitt));
        Instantiate(moving_platformprefab, new Vector2(14.63f, randomxpos), transform.rotation);
    }
    void spawnprojectiles()
    {
        float xposlimit = 0.46f;
        float xposlimitt = 1.64f;
        float randomxpos = (Random.Range(xposlimit, xposlimitt));
        Instantiate(projectilesprefab, new Vector2(14.63f, randomxpos), transform.rotation);
    }
}
    