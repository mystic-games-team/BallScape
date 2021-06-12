using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static public UIManager get;

    [Header("Game Objects")]
    [SerializeField] CanvasGroup deadMenu;
    [SerializeField] AudioSource song;
    [SerializeField] Image redBackground;
    [SerializeField] GameObject lifebar;
    [SerializeField] GameObject HUD;
    [SerializeField] Button continueLeaderboard;

    [Header("Leaderboard Menu")]
    [SerializeField] GameObject leaderboardMenu;
    [SerializeField] Button sumbitScore;
    [SerializeField] InputField usernameField;
    [SerializeField] GameObject boardRow;
    [SerializeField] Transform board;
    [SerializeField] Text scoreText;

    [Header("Values")]
    [SerializeField] float deadMenuAnimTime = 0.5f;
    [SerializeField] float alphaDeadMenuLimit = 0.9f;

    private void Awake()
    {
        get = this;
    }

    public void ShowDeadMenu()
    {
        StartCoroutine(DeadMenuAnim());
    }

    IEnumerator DeadMenuAnim()
    {
        Time.timeScale = 0;
        lifebar.SetActive(false);
        HUD.SetActive(false);

        float time = 0;
        float startVolume = song.volume;

        while (deadMenu.alpha < 1)
        {
            float t = (time += Time.unscaledDeltaTime) / deadMenuAnimTime;

            deadMenu.alpha = t < 1 ? t : 1;
            float a = redBackground.color.a;
            redBackground.color = new Color(1, 1, 1, a += Time.unscaledDeltaTime * alphaDeadMenuLimit);
            song.volume = Mathf.Lerp(startVolume, 0, t < 1 ? t : 1);

            yield return null;
        }

        deadMenu.interactable = true;
    }

    public void BlockStrangeChars(InputField inputField)
    {
        if (inputField.text.Length < 1)
        {
            return;
        }

        if (inputField.text[inputField.text.Length - 1] == '/' || inputField.text[inputField.text.Length - 1] == '|')
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1);
        }
    }

    public void StartSumbitScore()
    {
        if (usernameField.text != "")
            StartCoroutine(SumbitScore(usernameField.text, UIHUDKills.get.currentKills));
    }

    IEnumerator SumbitScore(string username, int score)
    {
        sumbitScore.interactable = false;

        string url = "https://ballscape.000webhostapp.com/UploadRecord.php";

        WWWForm w = new WWWForm();
        w.AddField("username", username);
        w.AddField("killAmmount", score);

        using (UnityWebRequest www = UnityWebRequest.Post(url, w))
        {
            yield return www.SendWebRequest();

            if (www.error != null)
            {
                Debug.Log("404 not found");
            }
            else
            {
                if (www.isDone)
                {
                    if (www.downloadHandler.text.Contains("Error"))
                    {
                        Debug.Log(www.downloadHandler.text);
                        sumbitScore.interactable = true;
                    }
                    else
                    {
                        sumbitScore.gameObject.SetActive(false);

                        for (int i = 0; i < board.childCount; ++i)
                        {
                            Destroy(board.GetChild(i).gameObject);
                        }

                        UpdateLeaderboard();
                    }
                }
            }
        }
    }

    public void UpdateLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    IEnumerator GetLeaderboard()
    {
        string url = "https://ballscape.000webhostapp.com/GetLeaderboard.php";
        UnityWebRequest w = UnityWebRequest.Get(url);

        yield return w.SendWebRequest();

        if (w.error != null)
        {
            Debug.Log("Error: " + w.error);
        }
        else
        {
            ShowLeaderboard(w.downloadHandler.text);
        }
        w.Dispose();
    }

    public void ShowLeaderboard(string data)
    {
        string username = "";
        string score = "";
        int lastChar = 0;

        for (int i = 0; i < 10; ++i)
        {
            string currentString = "";
            while (data[lastChar] != '|')
            {
                currentString += data[lastChar];
                ++lastChar;
            }

            username = currentString;
            currentString = "";
            ++lastChar;

            while (data[lastChar] != '/')
            {
                currentString += data[lastChar];
                ++lastChar;
            }

            score = currentString;
            ++lastChar;

            BoardInfo bi = Instantiate(boardRow, board).GetComponent<BoardInfo>();
            bi.SetBoard(username, score);
        }

        deadMenu.gameObject.SetActive(false);
        leaderboardMenu.SetActive(true);
    }

    public void GoToLeaderboard()
    {
        continueLeaderboard.interactable = false;
        scoreText.text = UIHUDKills.get.currentKills.ToString();
        UpdateLeaderboard();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

