using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDLifeBar : MonoBehaviour
{
    [Header("Life Quads Settings")]
    [SerializeField] GameObject lifeQuad;
    [SerializeField] GameObject lifeQuadsParent;

    [Header("Background")]
    [SerializeField] RectTransform background;

    [Header("Balance")]
    [SerializeField] int maxLife = 10;

    private List<GameObject> lifes = new List<GameObject>();

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
            DecreaseLifes(4);
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
    
    public bool DecreaseLife()
    {
        return DecreaseLifes(1);
    }

    public bool DecreaseLifes(int amout)
    {
        int min = Mathf.Min(amout, lifes.Count);
        for (int i = 0; i < min; ++i)
        {
            GameObject life = lifes[lifes.Count - 1];
            lifes.RemoveAt(lifes.Count - 1);
            Destroy(life);
        }
        return lifes.Count == 0;
    }
}
