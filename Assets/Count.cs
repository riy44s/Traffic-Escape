using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{

    void Update()
    {
        if (transform.childCount == 0)
        {
            GameManager.instance.LevelWinning();
        }
    }
}
