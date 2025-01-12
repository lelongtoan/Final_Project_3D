using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class ItemShopDetail : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    ShopData shopData;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI quanTxt;
    [SerializeField] Image iconImg;
    [SerializeField] Image iconPriceImg;
    [SerializeField] TextMeshProUGUI quanPriceTxt;
    [SerializeField] Button buttonBuy;
    [SerializeField] Button buttonWatch;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android"; // ID quảng cáo trên Android
    string _adUnitId = null;
    private void Awake()
    {
        buttonBuy.onClick.AddListener(Buy); 
        _adUnitId = _androidAdUnitId;
    }
    public void LoadAd()
    {
        // QUAN TRỌNG! Chỉ tải nội dung sau khi khởi tạo (trong ví dụ này, việc khởi tạo được xử lý ở một script khác).
        Debug.Log("Đang tải quảng cáo: " + _adUnitId); // Ghi lại ID quảng cáo đang tải
        Advertisement.Load(_adUnitId, this); // Tải quảng cáo với ID đã chọn
    }

    // Nếu quảng cáo tải thành công, thêm listener vào nút và bật nút lên.
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Quảng cáo đã tải thành công: " + adUnitId); // Ghi lại thông tin quảng cáo đã tải

        if (adUnitId.Equals(_adUnitId)) // Kiểm tra nếu ID quảng cáo tải thành công là đúng ID hiện tại
        {
            // Cấu hình nút để gọi phương thức ShowAd khi người dùng nhấn vào:
            buttonWatch.onClick.AddListener(ShowAd);
            // Bật nút để người dùng có thể nhấn vào:
            buttonWatch.interactable = true;
        }
    }

    // Thực thi khi người dùng nhấn vào nút:
    public void ShowAd()
    {
        // Tắt nút (người dùng không thể nhấn nữa khi quảng cáo đang hiển thị):
        buttonWatch.interactable = false;
        // Sau đó hiển thị quảng cáo:
        Advertisement.Show(_adUnitId, this);
    }

    // Xử lý kết quả khi quảng cáo hoàn thành (để xác định người dùng có được phần thưởng hay không):
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Quảng cáo thưởng Unity Ads đã hoàn thành.");
            if (shopData.buyType == ItemBuy.Nope)
            {
                if (shopData.perkData == null)
                {
                    Debug.LogError("Chua Them Perk hoac chua chinh buyType!!!");
                }
                if (shopData.perkData.perkState == PerkState.Lock)
                {
                    shopData.perkData.perkState = PerkState.Unlock;
                }
                shopData.perkData.quantity += shopData.quanlity;
            }
            else
            {
                if (shopData.buyType == ItemBuy.IronKey)
                {
                    MainMenuInstance.instance.inforMenu.ironKey += shopData.quanlity;
                }
                else if (shopData.buyType == ItemBuy.SilverKey)
                {
                    MainMenuInstance.instance.inforMenu.silverKey += shopData.quanlity;
                }
                else
                {
                    MainMenuInstance.instance.inforMenu.diamond += shopData.quanlity;
                }
            }
            // Cấp phần thưởng cho người chơi (có thể là tiền, vật phẩm,...)
        }
    }

    // Xử lý khi tải quảng cáo gặp lỗi:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Lỗi khi tải quảng cáo ID {adUnitId}: {error.ToString()} - {message}");
        // Sử dụng chi tiết lỗi để quyết định có nên tải lại quảng cáo hay không.
    }

    // Xử lý khi hiển thị quảng cáo gặp lỗi:
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Lỗi khi hiển thị quảng cáo ID {adUnitId}: {error.ToString()} - {message}");
        // Sử dụng chi tiết lỗi để quyết định có nên thử hiển thị quảng cáo lại không.
    }

    // Các phương thức còn lại của IUnityAdsShowListener:
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Dọn dẹp các listener của nút khi đối tượng bị hủy:
        buttonWatch.onClick.RemoveAllListeners();
    }
    public void Set(ShopData data)
    {
        shopData = data;
        nameTxt.text = data.itemName;
        quanTxt.text = data.quanlity.ToString();
        iconImg.sprite = data.imageItemBuy;
        iconPriceImg.sprite = data.imagePrice;
        quanPriceTxt.text = data.price.ToString();
        if (shopData.buyType == ItemBuy.Nope)
        {
            iconImg.sprite = data.perkData.image;
        }
        if(data.priceType == StatePrice.Nope)
        {
            buttonBuy.gameObject.SetActive(false);
            buttonWatch.gameObject.SetActive(true);
            LoadAd();
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
            buttonWatch.gameObject.SetActive(false);
        }
    }
    public void Buy()
    {
        if(shopData.priceType == StatePrice.Money)
        {
            if (MainMenuInstance.instance.inforMenu.money < shopData.price)
                return;
            MainMenuInstance.instance.inforMenu.money -= shopData.price;
            ReportMain.instance.SetReport("Đã Mua Thành Công.");
        }
        else if(shopData.priceType == StatePrice.Diamond)
        {
            if (MainMenuInstance.instance.inforMenu.diamond < shopData.price)
                return;
            MainMenuInstance.instance.inforMenu.diamond -= shopData.price;
            ReportMain.instance.SetReport("Đã Mua Thành Công.");
        }

        if (shopData.buyType == ItemBuy.Nope)
        {
            if(shopData.perkData == null)
            {
                Debug.LogError("Chua Them Perk hoac chua chinh buyType!!!");
            }
            if(shopData.perkData.perkState == PerkState.Lock)
            {
                shopData.perkData.perkState = PerkState.Unlock;
            }
            shopData.perkData.quantity += shopData.quanlity;
        }
        else 
        {
            if(shopData.buyType == ItemBuy.IronKey)
            {
                MainMenuInstance.instance.inforMenu.ironKey += shopData.quanlity;
            }
            else if(shopData.buyType == ItemBuy.SilverKey)
            {
                MainMenuInstance.instance.inforMenu.silverKey += shopData.quanlity;
            }
            else
            {
                MainMenuInstance.instance.inforMenu.diamond += shopData.quanlity;
            }
        }
    }
}
