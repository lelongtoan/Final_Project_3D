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
    public GameObject newPlayer;
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
        levelText.gameObject.SetActive(false);
        pointText.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        nameText.text = "New Game";

        if (startData.data[id] != null && startData.data[id].isSave) 
        {
            avatar.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            pointText.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);

            //avatar.sprite = saveData.saveDatas[id].avatar;
            nameText.text = "Knight";
            levelText.text = "Level : " + startData.data[id].level;
            pointText.text = "Point : " + startData.data[id].point;
        }
    }
    public void SetPlayer()
    {
        if (startData.data[id].isSave == false)
        {
            newPlayer.SetActive(true);
        }
        SaveInGame.instance.SetCharSave(id);
    }
    public void SetDeletePlayer()
    {
        SaveInGame.instance.DetelePlayer(id);
        SaveInGame.instance.LoadPanelChar();
        Set();
    }
}
