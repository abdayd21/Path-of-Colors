using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagger : MonoBehaviour
{

    public static Vector2 lastCheckpointPos = new Vector2(-3, 0);

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
