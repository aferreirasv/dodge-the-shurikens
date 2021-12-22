using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldFood:MonoBehaviour
{

    public Rigidbody2D rb;
    public float value;
    GameManager gameManager;
    public AudioSource audioSource;
    GameObject gameManagerObj;

    public void Start()
    {
        gameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        if (gameManagerObj != null)
        {
            Create();
        }
        else
        {
            Debug.LogError("GameManager not Found with tag \"gamemanager\"");
        }
    }




    public void Create()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
        rb.position = gameManager.GetRandomPosition();
        value = gameManager.level * 2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<Rigidbody2D>().transform.localScale += new Vector3(value / 300f, value / 300f, 0);
            Player player = other.GetComponent<Player>();
            player.SetShield(true);
            gameManager.UpdateScore((int)(value * 100f));
            audioSource.Play();
            Destroy(gameObject);
        }
    }





    // Update is called once per frame


}
