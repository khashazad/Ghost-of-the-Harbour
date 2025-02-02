using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        heartContainers = new GameObject[2];  // Always 2 hearts as per your initial setup
        heartFills = new Image[2];

        // Subscribe to the event from PlayerScript
        PlayerScript.LivesChangedCallback += UpdateHeartsHUD;

        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        // Show hearts as long as the lives count is <= 2
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < player.GetComponent<PlayerScript>().lives) 
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < player.GetComponent<PlayerScript>().lives)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        PlayerScript.LivesChangedCallback -= UpdateHeartsHUD;
    }
}
