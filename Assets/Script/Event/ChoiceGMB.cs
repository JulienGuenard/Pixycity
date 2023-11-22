using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceGMB : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image btnImage;
    [SerializeField] private EventGMB eventGMB;
    private ChoiceObject choiceObj;
    #endregion

    #region Get / Set
    public TextMeshProUGUI Title
    {
        get { return title; }
        set { title = value; }
    }
    public Image BtnImage
    {
        get { return btnImage; }
        set { btnImage = value; }
    }
    public EventGMB EventGMB
    {
        get { return eventGMB; }
        set { eventGMB = value; }
    }
    public ChoiceObject ChoiceObj
    {
        get { return choiceObj; }
        set
        {
            choiceObj = value;

            if (value != null)
            {
                Title.text = value.name;
                BtnImage.sprite = value.infos.spriteIcon;
                BtnImage.color = Color.white;
            }
            else
            {
                Title.text = null;
                BtnImage.sprite = null;
                BtnImage.color = Color.black;
            }
        }
    }
    #endregion
}
