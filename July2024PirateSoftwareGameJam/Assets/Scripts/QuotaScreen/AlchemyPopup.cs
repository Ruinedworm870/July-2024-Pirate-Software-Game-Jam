using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyPopup : MonoBehaviour
{
    public Animator controller;

    public LoadMainScreen loadMainScreen;

    public Sprite impureIronSprite;
    public Sprite ironChunkSprite;
    public Sprite pureIronPlateSprite;

    public Image materialSpriteHolder;
    public TextMeshProUGUI materialAmountText;

    public Image materialSliderImage;
    public TextMeshProUGUI materialSliderAmount;
    public TextMeshProUGUI goldSliderAmount;
    public Slider slider;

    /*
        Options:
        1 = Impure Iron
        2 = Iron Chunk
        3 = Pure Iron Plate
    */
    private int openOption;
    private int sliderValue;
    private bool isOpen = false;
    
    public void OpenPopup(int option)
    {
        if(!isOpen)
        {
            isOpen = true;
            openOption = option;
            SetupMenu();
            controller.SetTrigger("OpenPopup");
        }
        
    }

    public void ClosePopup()
    {
        if(isOpen)
        {
            isOpen = false;
            controller.SetTrigger("ClosePopup");
        }
    }
    
    public void Transmutate()
    {
        int materialAmount = DataHandler.Instance.resourceInfo.GetConversionRate(openOption) * sliderValue;
        int newAmount = DataHandler.Instance.resourceInfo.GetAmount(openOption) - materialAmount;
        DataHandler.Instance.resourceInfo.SetAmount(openOption, newAmount);

        int newGold = DataHandler.Instance.resourceInfo.GetAmount(0) + sliderValue;
        DataHandler.Instance.resourceInfo.SetAmount(0, newGold);

        loadMainScreen.LoadQuotaInfo();
        loadMainScreen.LoadResourceInfo();

        ClosePopup();
    }

    private void SetupMenu()
    {
        Sprite sprite = GetMaterialSprite(openOption);
        materialSpriteHolder.sprite = sprite;
        materialSliderImage.sprite = sprite;

        string materialName = GetMaterialName(openOption);
        materialSpriteHolder.GetComponent<Tooltip>().SetText(materialName);
        materialSliderImage.GetComponent<Tooltip>().SetText(materialName);

        int conversionRate = DataHandler.Instance.resourceInfo.GetConversionRate(openOption);
        materialAmountText.text = conversionRate + "x";

        int maxSliderValue = DataHandler.Instance.resourceInfo.GetAmount(openOption) / conversionRate;
        slider.maxValue = maxSliderValue;
        slider.value = 0;
        sliderValue = 0;

        materialSliderAmount.text = "-0";
        goldSliderAmount.text = "+0";
    }
    
    public void OnSliderChange(float value)
    {
        sliderValue = (int)value;
        
        int materialAmount = DataHandler.Instance.resourceInfo.GetConversionRate(openOption) * sliderValue;
        materialSliderAmount.text = "-" + NumberHandler.GetDisplay(materialAmount, 1);

        goldSliderAmount.text = "+" + NumberHandler.GetDisplay(sliderValue, 1);
    }

    private Sprite GetMaterialSprite(int option)
    {
        switch(option)
        {
            case 1:
                return impureIronSprite;
            
            case 2:
                return ironChunkSprite;
            
            case 3:
                return pureIronPlateSprite;
        }
        
        return null;
    }

    private string GetMaterialName(int option)
    {
        switch (option)
        {
            case 1:
                return "Impure Iron";

            case 2:
                return "Iron Chunk";

            case 3:
                return "Pure Iron Plate";
        }

        return null;
    }
}
