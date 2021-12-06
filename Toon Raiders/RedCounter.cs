using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCounter : MonoBehaviour
{
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "RedCounter")
        {
            //Destroy object when it enters Player 1's goal
            gameObject.SetActive(false);

            //Update UI
            ChaliceManager.instance.AddRedPoint();
        }
    }
}
