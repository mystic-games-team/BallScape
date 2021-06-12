using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUDKills : MonoBehaviour
{
    public static UIHUDKills get { get; private set; }

    [SerializeField] TextMeshProUGUI kills;

    int currentKills = 0;

    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        kills.text = "0";
    }

    public void AddKill()
    {
        ++currentKills;
        kills.text = currentKills.ToString();
    }
}
