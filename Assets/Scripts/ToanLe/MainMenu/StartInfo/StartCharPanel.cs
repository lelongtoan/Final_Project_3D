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
    public Button deleteButton;
    public Button button;
    private void OnEnable()
    {
        Set();
    }
    public void Set()
    {
        avatar.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        pointText.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        if (startInfo.isSave)
        {
            avatar.gameObject.SetActive(true);
            nameText.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            pointText.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);

            avatar.sprite = startInfo.avatar;
            nameText.text = startInfo.nameChar;
            levelText.text = "Level : " + startInfo.level;
            pointText.text = "Point : " + startInfo.point;
        }
        button.onClick.AddListener(SetPlayer);
        deleteButton.onClick.AddListener(SetDeletePlayer);
    }
    public void SetPlayer()
    {
        SaveInGame.instance.SetCharSave(id);
    }
    public void SetDeletePlayer()
    {
        SaveInGame.instance.DetelePlayer(id);
        startInfo.isSave = false;
        Set();
    }
}
