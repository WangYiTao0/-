using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmGame
{


public class TimeUI : MonoBehaviour
{
    [SerializeField] private RectTransform _dayNightImage;
    [SerializeField] private RectTransform _clockParent;
    [SerializeField] private Image _seasonImage;
    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private TextMeshProUGUI _timeText;

    [SerializeField] private Sprite[] _seasonSprites;

    private List<GameObject> _clockBlocks = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _clockParent.childCount; i++)
        {
            _clockBlocks.Add(_clockParent.GetChild(i).gameObject);
            _clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
        EventHandler.GameDateEvent += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }
    private void OnGameMinuteEvent(int minute, int hour)
    {
        _timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    }

    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        _dateText.text = year + "年" + month.ToString("00") + "月" + day.ToString("00") + "日";
        _seasonImage.sprite = _seasonSprites[(int)season];

        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }

    /// <summary>
    /// 根据小时切换时间块显示
    /// </summary>
    /// <param name="hour"></param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;

        if (index == 0)
        {
            foreach (var item in _clockBlocks)
            {
                item.SetActive(false);
            }
        }
        else
        {
      
            for (int i = 0; i < _clockBlocks.Count; i++)
            {
                //序号小于当期那index
                if (i < index + 1)
                    _clockBlocks[i].SetActive(true);
                else
                    _clockBlocks[i].SetActive(false);
            }
        }
    }

    private void DayNightImageRotate(int hour)
    {
        var target = new Vector3(0, 0, hour * 15 - 90);
        _dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }
}
}