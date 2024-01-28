using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{

    public int player1 = 0;
    public int player2 = 0;

    public Text Player1Score;
    public Text Player2Score;

    public GameObject aiPlayer;

    public void Start()
    {
        Player1Score.text = $"PLAYER 1 SCORE: {player1.ToString()}";
        Player2Score.text = $"PLAYER 2 SCORE: {player2.ToString()}";
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall Left")
        {

            ResetBallPositionAfterGoal();

            player2++;
            Player2Score.text = $"PLAYER 2 SCORE: {player2.ToString()}";
            PlayerWins();

        }

        else if (collision.gameObject.name == "Wall Right")
        {

            ResetBallPositionAfterGoal();

            player1++;
            Player1Score.text = $"PLAYER 1 SCORE: {player1.ToString()}";
            PlayerWins();
        }
    }

    public void ResetBallPositionAfterGoal()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().transform.position = new Vector3(0, -1, 1);
        
        // onGamePointEnd
        PlayerManagement playerManagement = aiPlayer.GetComponent<PlayerManagement>();
        playerManagement.OnGamePointEnd();
    }

    public void PlayerWins()
    {
        if (player1 >= 10 || player2 >= 10)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
