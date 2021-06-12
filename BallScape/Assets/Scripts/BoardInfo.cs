using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardInfo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Text username;
    [SerializeField] Text score;

    public void SetBoard(string username, string score)
    {
        this.username.text = username;
        this.score.text = score;
    }
}
