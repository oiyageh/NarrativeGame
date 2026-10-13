using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class YarnBg : MonoBehaviour
{
    public static YarnBg Instance;

    [System.Serializable]
    public class Background
    {
        public string backgroundName;
        public Sprite image;
    }

    public Image backgroundImage;
    public Background[] backgrounds;

    //private void Start()
    //{
    //    backgroundImage.sprite = backgrounds[1].image;

    //    Debug.Log("TEST IMAGE 2 CHANGED!");
    //}

    private void Awake()
    {
        Instance = this;

        Debug.Log("Yarn Background started!");
    }

    [YarnCommand("background")]
    public static void ChangeBackground(string backgroundName)
    {
        Debug.Log("BACKGROUND COMMAND RECEIVED: " + backgroundName);

        if (Instance == null)
        {
            Debug.LogError("YarnBackgroundManager Instance is NULL!");
            return;
        }

        foreach (Background background in Instance.backgrounds)
        {
            if (background.backgroundName == backgroundName)
            {
                Instance.backgroundImage.sprite = background.image;

                // Make sure the image is visible
                Instance.backgroundImage.color = Color.white;
                Instance.backgroundImage.enabled = true;

                Debug.Log("BACKGROUND CHANGED TO: " + backgroundName);

                return;
            }
        }

        Debug.LogWarning("BACKGROUND NOT FOUND: " + backgroundName);
    }
}