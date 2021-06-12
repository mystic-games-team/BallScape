using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager get;

    [Header("Game Objects")]
    [SerializeField] CanvasGroup deadMenu;
    [SerializeField] AudioSource song;
    [SerializeField] Image redBackground;
    [SerializeField] GameObject lifebar;
    [SerializeField] GameObject HUD;

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
        lifebar.SetActive(false);
        HUD.SetActive(false);

        float time = 0;
        float startVolume = song.volume;

        while (deadMenu.alpha < 1)
        {
            float t = (time += Time.deltaTime) / deadMenuAnimTime;

            deadMenu.alpha = t < 1 ? t : 1;
            float a = redBackground.color.a;
            redBackground.color = new Color(1, 1, 1, a += Time.deltaTime * 0.9f);
            song.volume = Mathf.Lerp(startVolume, 0, t < 1 ? t  : 1);

            yield return null;
        }

        deadMenu.interactable = true;
    }
}
