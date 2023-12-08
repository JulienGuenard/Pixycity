using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeteoGMB : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image meteoImage;
    [SerializeField] private MeteoObject meteoObject;
    [SerializeField] private RectTransform rectBorder;
    [SerializeField] private TooltipTrigger tooltip;
    private RectTransform rect;
    #endregion

    #region Get / Set
    public Image MeteoImage
    {
        get { return meteoImage; }
        set { meteoImage = value; }
    }
    public MeteoObject MeteoObject
    {
        get { return meteoObject; }
        set 
        { 
            meteoObject = value;
            MeteoImage.sprite = value.infos.infos.spriteIcon;
            tooltip.feedback = value.infos;
        }
    }
    public RectTransform RectBorder
    {
        get { return rectBorder; }
        set { rectBorder = value; }
    }
    public RectTransform Rect
    {
        get { return rect; }
        set { rect = value; }
    }
    #endregion

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
