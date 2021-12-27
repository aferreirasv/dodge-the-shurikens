using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float value;
    GameManager gameManager;
    AudioSource audioSource;
    public Light light;
    private void Start()
    {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        audioSource = GetComponent<AudioSource>();
        light = GetComponent<Light>();
        if (gameManagerObj != null)
        {
            gameManager = gameManagerObj.GetComponent<GameManager>();
            rb = GetComponent<Rigidbody2D>();
            Create();
        }
        else
        {
            Debug.LogError("GameManager not Found with tag \"gamemanager\"");
        }
    }

    public void Create()
    {
        rb.transform.localScale = new Vector3(1f, 1f, 1f);
        rb.position = gameManager.GetRandomPosition();
        value = Random.Range(1f,gameManager.level + 1);
        rb.transform.localScale += new Vector3((value / 6f) - 0.1f, (value /6f) - 0.1f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player")
        {
            other.GetComponent<Rigidbody2D>().transform.localScale += new Vector3(value / 300f, value / 300f, 0);
            gameManager.UpdateScore((int)(value*100f));
            audioSource.Play();
            Create();
        }
    }





    // Update is called once per frame


}
