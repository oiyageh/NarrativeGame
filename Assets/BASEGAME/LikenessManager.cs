using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;
using System.Collections;

public class LikenessManager : MonoBehaviour
{
    public static LikenessManager Instance;

    [Header("Likeness")]
    public int likeness = 50;
    public int maxLikeness = 100;
    public int minLikeness = 0;

    [Header("UI")]
    public Slider likenessSlider;
    public TMP_Text likenessText;

    [Header("Decrease Popup")]
    public GameObject decreasePopup;
    public TMP_Text decreaseText;

    private Coroutine popupCoroutine;

    private void Awake()
    {
        Instance = this;

        UpdateLikenessUI();

        if (decreasePopup != null)
        {
            decreasePopup.SetActive(false);
        }
    }

    [YarnCommand("likeness")]
    public static void ChangeLikeness(int amount)
    {
        Debug.Log("LIKENESS COMMAND RECEIVED: " + amount);

        if (Instance == null)
        {
            Debug.LogError("LikenessManager Instance is NULL!");
            return;
        }

        int oldLikeness = Instance.likeness;

        Instance.likeness += amount;

        Instance.likeness = Mathf.Clamp(
            Instance.likeness,
            Instance.minLikeness,
            Instance.maxLikeness
        );

        Instance.UpdateLikenessUI();

        Debug.Log(
            "Likeness changed from " +
            oldLikeness +
            " to " +
            Instance.likeness
        );

        if (amount < 0)
        {
            Instance.ShowDecreasePopup(amount);
        }
    }

    private void UpdateLikenessUI()
    {
        if (likenessSlider != null)
        {
            likenessSlider.maxValue = maxLikeness;
            likenessSlider.value = likeness;
        }

        if (likenessText != null)
        {
            likenessText.text = likeness + " / " + maxLikeness;
        }
    }

    private void ShowDecreasePopup(int amount)
    {
        if (decreasePopup == null || decreaseText == null)
        {
            return;
        }

        decreaseText.text = "Likeness " + amount;

        decreasePopup.SetActive(true);

        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine = StartCoroutine(HidePopup());
    }

    private IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(2f);

        decreasePopup.SetActive(false);
    }
}