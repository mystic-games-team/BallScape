using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDLifeBar : MonoBehaviour
{
    public static UIHUDLifeBar get { get; private set; }

    [Header("Life Quads Settings")]
    [SerializeField] GameObject lifeQuad;
    [SerializeField] GameObject lifeQuadsParent;

    [Header("Background")]
    [SerializeField] RectTransform background;

    [Header("Balance")]
    [SerializeField] int maxLife = 10;

    private List<GameObject> lifes = new List<GameObject>();

    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in lifeQuadsParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxLife; ++i)
        {
            lifes.Add(Instantiate(lifeQuad, lifeQuadsParent.transform));
        }

        StartCoroutine(UpdateBackground());
    }

    IEnumerator UpdateBackground()
    {
        yield return new WaitForEndOfFrame();

        HorizontalLayoutGroup group = lifeQuadsParent.GetComponent<HorizontalLayoutGroup>();
        background.sizeDelta = new Vector2(group.preferredWidth, group.preferredHeight);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddLifes(3);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerController.get.RecieveDamage(4);
        }
    }
#endif

    public void AddLife()
    {
        AddLifes(1);
    }

    public void AddLifes(int amount)
    {
        int max = Mathf.Min(amount, maxLife - lifes.Count);
        for (int i = 0; i < max; ++i)
        {
            lifes.Add(Instantiate(lifeQuad, lifeQuadsParent.transform));
        }
    }
    
    public void DecreaseLife(Action callbackOnDead)
    {
        DecreaseLifes(1, callbackOnDead);
    }

    public void DecreaseLifes(int amout, Action callbackOnDead)
    {
        int min = Mathf.Min(amout, lifes.Count);
        for (int i = 0; i < min; ++i)
        {
            GameObject life = lifes[lifes.Count - 1];
            lifes.RemoveAt(lifes.Count - 1);
            Destroy(life);
        }

        if (lifes.Count == 0)
        {
            callbackOnDead?.Invoke();
        }
    }
}
