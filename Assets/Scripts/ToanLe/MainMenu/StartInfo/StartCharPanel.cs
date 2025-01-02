using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartCharPanel : MonoBehaviour
{
    public int id;
    public StartData startData;
    public Image avatar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI pointText;
    public Button deleteButton;
    public Button button;
    private void Awake()
    {
        button.onClick.AddListener(SetPlayer);
        deleteButton.onClick.AddListener(SetDeletePlayer);
    }
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
        if (startData.data[id] != null && startData.data[id].isSave) 
        {
            avatar.gameObject.SetActive(true);
            nameText.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            pointText.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);

            //avatar.sprite = saveData.saveDatas[id].avatar;
            //nameText.text = saveData.saveDatas[id].nameChar;
            levelText.text = "Level : " + startData.data[id].level;
            pointText.text = "Point : " + startData.data[id].point;
        }
    }
    public void SetPlayer()
    {
        SaveInGame.instance.SetCharSave(id);
    }
    public void SetDeletePlayer()
    {
        SaveInGame.instance.DetelePlayer(id);
        SaveInGame.instance.LoadPanelChar();
        Set();
    }
}
