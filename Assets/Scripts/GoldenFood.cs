using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFood : MonoBehaviour
{

    public Rigidbody2D rb;
    public float value;
    public float timeLeft = 10f;
    public TextMesh countdownText;
    GameManager gameManager;
    AudioSource audioSource;

    private void Start()
    {
        countdownText = GetComponentInChildren<TextMesh>();
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("gamemanager");
        audioSource = GetComponent<AudioSource>();
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

    private void Update()
    {

        timeLeft -= Time.deltaTime;
        countdownText.text = timeLeft.ToString("0.0");
        if(timeLeft < 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Create()
    {
        rb.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        rb.position = gameManager.GetRandomPosition();
        value = gameManager.level * 2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<Rigidbody2D>().transform.localScale += new Vector3(value / 300f, value /300f, 0);
            other.GetComponent<Player>().AddSpeed(2);
            gameManager.UpdateScore((int)(value * 100f));
            audioSource.Play();
            Destroy(this.gameObject);
        }
    }





    // Update is called once per frame


}
