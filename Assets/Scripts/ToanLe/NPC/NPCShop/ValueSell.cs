using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueSell : MonoBehaviour
{
    
    public TMP_InputField inputField;
    public TextMeshProUGUI totalPrice;
    public float price = 0;
    public int minValue = 1;
    public int maxValue = 99;
    public int quantityItem = 1;
    private void Start()
    {
        inputField.onEndEdit.AddListener(OnInputValueChanged);
        maxValue = GameInstance.instance.itemD.itemSlot.count;
        price = GameInstance.instance.itemD.itemSlot.item.gold;
        totalPrice.text = price.ToString();
    }
    private void OnInputValueChanged(string value)
    {
        if (int.TryParse(value, out int quantity))
        {
            quantity = Mathf.Clamp(quantity, minValue, maxValue);

            inputField.text = quantity.ToString();
            price = GameInstance.instance.itemD.itemSlot.item.gold * quantity;
            quantityItem = quantity;
        }
        else
        {
            GameInstance.instance.gameReport.SetReport("Hãy Nhập Số!");
            inputField.text = minValue.ToString();
        }
        totalPrice.text = price + "";
    }
}
