using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartCharPanel : MonoBehaviour
{
    public int id;
    public StartInfo startInfo;
    public Image avatar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI pointText;

    private void Start()
    {
        avatar.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        pointText.gameObject.SetActive(false);
        if (startInfo.avatar != null)
        {
            avatar.gameObject.SetActive(true);
            nameText.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            pointText.gameObject.SetActive(true);
            avatar.sprite = startInfo.avatar;
            nameText.text = startInfo.nameChar;
            levelText.text = "Level : " + startInfo.level;
            pointText.text = "Point : " + startInfo.point;
        }
    }
}
