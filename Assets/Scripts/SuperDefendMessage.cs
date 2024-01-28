using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperDefendMessage : MonoBehaviour
{
    public Text startText;
    public GameObject aiPlayer;

    private bool isBlinking = false;
    

    public IEnumerator Blink()
    {
        while (true)
        {
            startText.color = Color.green;
            yield return new WaitForSeconds(0.1f);
            startText.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            startText.color = Color.blue;
            yield return new WaitForSeconds(0.1f);
            startText.color = Color.magenta;
            yield return new WaitForSeconds(0.1f);
            startText.color = Color.cyan;
            yield return new WaitForSeconds(0.1f);
            startText.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void StartBlinking()
    {
        StartCoroutine("Blink");
        isBlinking = true;
        startText.text = "SUPER DEFENSE";
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
        isBlinking = false;
        startText.text = "";
    }

    public void Update()
    {
        if (aiPlayer.GetComponent<PlayerManagement>().isSuperDefend)
        {
            if (!isBlinking) StartBlinking();
        }
        else

        {
            StopBlinking();
        }
    }
}