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



    void Start()
    {
        augmentUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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