using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PlaySound(int index)
    {
        AudioManager.instance.PlaySound(transform.position, index, Random.Range(1f, 1f), 0.01f);

    }

}
