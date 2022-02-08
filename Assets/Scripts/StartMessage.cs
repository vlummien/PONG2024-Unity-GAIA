using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMessage : MonoBehaviour
{

    public Text startText;

    public void Start()
    {
        StartBlinking();
    }
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
    }
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }

    public void Update()
    {

        if (Input.GetKeyUp(KeyCode.Return))
        {
            startText.text = "";
            StopBlinking();
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
