using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Vector2 MoveVector;

    [SerializeField]
    ObstacleMover obstacleMover;

    [SerializeField]
    AudioClip moveUpClip;

    [SerializeField]
    AudioClip deadClip;

    [SerializeField]
    AudioClip scoreClip;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text highscoreText;

    bool isEnded = false;
    Rigidbody2D rigidbody;
    AudioSource audioSource;

    int score = 0;
    const string HighscoreKey = "Highscore";
    float edgeOfScene = 6;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        SetScoreText(0);
        SetHighScoreText(HighscoreKey,0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MoveUp();

        if (Input.GetMouseButtonDown(0))
            MoveUp();
    }

    void MoveUp()
    {

        if (isEnded)
            return;
        //Transform transform = GetComponent<Transform>();
        //transform.position = new Vector3(-5f, 2.5f, 0);
        //transform.position = transform.position + new Vector3(0, 1f, 0);
        //rigidbody.AddForce(MoveVector);

        Vector2 LimitMoveVector = MoveVector;//Limit to Flight of a bird
        if (rigidbody.position.y >= edgeOfScene)
        {
            LimitMoveVector.y = 0;
            rigidbody.velocity = LimitMoveVector;
        }
        else
        {
            rigidbody.velocity = MoveVector;
        }
        
        Debug.Log(rigidbody.position.y);
        audioSource.PlayOneShot(moveUpClip);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            audioSource.PlayOneShot(scoreClip);
            score++;
            SetScoreText(score);
        }

    }

    void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    void SetHighScoreText(string highscoreKey,int score)
    {
        highscoreText.text = PlayerPrefs.GetInt(highscoreKey, score).ToString();  
    }

    void SetHighScore(string highscoreKey, int score)
    {
        int highscore = PlayerPrefs.GetInt(highscoreKey, score);
        if (score > highscore)
        {
            PlayerPrefs.SetInt(highscoreKey, score);
        }
        else if (score <= highscore)
        {
            PlayerPrefs.SetInt(highscoreKey, highscore); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (isEnded)
            return;

        Debug.Log("You lose!");
        isEnded = true;
        audioSource.PlayOneShot(deadClip);

        SetHighScore(HighscoreKey, score);

        //GameObject.Find("Mover").GetComponent<ObstacleMover>().Stop();
        //GameObject.FindObjectOfType<ObstacleMover>().Stop();

        //collision.gameObject.transform.parent.parent.GetComponent<ObstacleMover>().Stop();

        /*
        if( collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.transform.parent.parent.GetComponent<ObstacleMover>().Stop();
        }
        else if(collision.gameObject.CompareTag("Ground") )
        {
            GameObject.Find("Mover").GetComponent<ObstacleMover>().Stop();
        }
        else
        {
            Debug.Log("Unknown collision " + collision.gameObject.name);
        }
        */

        obstacleMover.Stop();
    }
}
