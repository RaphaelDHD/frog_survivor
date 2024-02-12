using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentManager : MonoBehaviour
{
    // Start is called before the first frame

    public List<Augment> listAugment;
    public GameObject augmentUI;
    public Image augmentIcon1;
    public Image augmentIcon2;
    public Image augmentIcon3;
    public TextMeshProUGUI augmentContent1;
    public TextMeshProUGUI augmentContent2;
    public TextMeshProUGUI augmentContent3;

    public Button augmentButton1;
    public Button augmentButton2;
    public Button augmentButton3;

    public GameObject joystick;


    private static AugmentManager _instance;
    public static AugmentManager Instance { get { return _instance; } }



    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
    }


    void Start()
    {
        augmentUI.SetActive(false);
    }


    public void playerLevelledUp()
    {

        Time.timeScale = 0;

        joystick.SetActive(false);

        List<Augment> selectedAugments = SelectThreeRandomAugment();

        DisplayAugment(selectedAugments[0], augmentContent1, augmentIcon1);
        DisplayAugment(selectedAugments[1], augmentContent2, augmentIcon2);
        DisplayAugment(selectedAugments[2], augmentContent3, augmentIcon3);

        augmentButton1.onClick.AddListener(() => ProcessAugment(selectedAugments[0]));
        augmentButton2.onClick.AddListener(() => ProcessAugment(selectedAugments[1]));
        augmentButton3.onClick.AddListener(() => ProcessAugment(selectedAugments[2]));

        augmentUI.SetActive(true);

    }


    List<Augment> SelectThreeRandomAugment()
    {
        List<Augment> selectedAugments = new List<Augment>();

        while (selectedAugments.Count < 3)
        {
            Augment randomAugment = listAugment[Random.Range(0, listAugment.Count)];
            if (!selectedAugments.Contains(randomAugment))
            {
                selectedAugments.Add(randomAugment);
            }
        }

        return selectedAugments;
    }

    void DisplayAugment(Augment augment, TextMeshProUGUI contentText, Image iconImage)
    {
        contentText.text = augment.augmentContent;
        iconImage.sprite = augment.augmentIcon;
    }

    void ProcessAugment(Augment augment)
    {
        // Example process: just print the augment type and value
        Debug.Log("Processing Augment: Type - " + augment.augmentType + ", Value - " + augment.augmentValue);

        switch (augment.augmentType)
        {
            case "health":
                PlayerManager.Instance.gainMaxHealth((augment.augmentValue * PlayerManager.Instance.maxHealth) / 100);
                break;
            case "heal":
                PlayerManager.Instance.recoverLife(augment.augmentValue);
                break;
            case "attack":
                PlayerManager.Instance.damage += (augment.augmentValue * PlayerManager.Instance.damage) / 100;
                break;
            case "attackSpeed":
                PlayerManager.Instance.attackSpeed -= (augment.augmentValue * PlayerManager.Instance.attackSpeed) / 100;
                break;
            case "attackCrit":
                PlayerManager.Instance.criticalChance += augment.augmentValue;
                break;
            case "speed":
                PlayerManager.Instance.speed += (float)(augment.augmentValue * PlayerManager.Instance.speed) / 100;
                break;
        }



        joystick.SetActive(true);
        augmentUI.SetActive(false);  
        Time.timeScale = 1;

    }




}


[System.Serializable]
public class Augment
{
    public string augmentContent;
    public Sprite augmentIcon;
    public string augmentType;
    public int augmentValue;
}