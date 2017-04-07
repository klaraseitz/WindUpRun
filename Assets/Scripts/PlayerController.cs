using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject winText;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject restartButton;
    public float numberOfSecondsToWin;
    

    private Rigidbody2D rb2d;
    private bool winning;
    private float currentSecondsToWin;

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        winning = false;
        winText.SetActive(false);
        restartButton.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        currentSecondsToWin = numberOfSecondsToWin;
        Debug.Log("initial position of rb2d: " + rb2d.position.x.ToString() + ", " + rb2d.position.y.ToString());
        
    }

    private void Update()
    {
        
        if (winning && currentSecondsToWin > 0)
        {
            Debug.Log("currentSecondsToWin: " + currentSecondsToWin.ToString());
            // player reached end field, has to stay on field to win a certain time
            currentSecondsToWin -= Time.deltaTime;
            if (currentSecondsToWin > 2 && currentSecondsToWin <= 3)
            {
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
            } else if (currentSecondsToWin > 1 && currentSecondsToWin <= 2)
            {
                text1.SetActive(false);
                text2.SetActive(true);
                text3.SetActive(false);
            } else
            {
                text1.SetActive(true);
                text2.SetActive(false);
                text3.SetActive(false);
            }
        }else if (currentSecondsToWin <= 0)
        {
            // player won game
            text1.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(false);
            winText.SetActive(true);
            restartButton.SetActive(true);
        }
        
        
    }

    void FixedUpdate()
    {      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            //transform.position = touchPosition;
            rb2d.MovePosition(new Vector2(touchPosition.x, touchPosition.y));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndMarker"))
        {
            Debug.Log("Player collided with endMarker");
            winning = true;
            text1.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(true);
        } else if (other.gameObject.CompareTag("NotEndMarker"))
        {
            Debug.Log("Player collided with NotEndMarker");
            winning = false;
            text1.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(false);
            currentSecondsToWin = numberOfSecondsToWin;
        }
    }

    public void Restart()
    {
        winning = false;
        currentSecondsToWin = numberOfSecondsToWin;
        rb2d.MovePosition(new Vector2(-7.67f, 0f));
        winText.SetActive(false);
        restartButton.SetActive(false);
    }
    
}
