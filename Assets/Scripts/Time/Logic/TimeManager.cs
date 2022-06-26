using System;
using UnityEngine;

namespace FarmGame
{
    public class TimeManager : MonoBehaviour
    {
        private int _gameSecond, _gameMinute, _gameHour, _gameDay, _gameMonth, _gameYear;
        private Season _season = Season.Spring;

        private int _mouthInSeason = 3;

        public bool _gameClockPause;

        private float _tickTime;
        //

        private void Awake()
        {
            NewGameTime();
        }

        private void Start()
        {
            EventHandler.CallGameMinuteEvent(_gameMinute,_gameHour);
            
            EventHandler.CallGameDateEvent(
                _gameHour,_gameDay,_gameMonth,_gameYear,_season);
        }

        private void NewGameTime()
        {
            _gameSecond = 0;
            _gameMinute = 0;
            _gameHour = 7;
            _gameDay = 1;
            _gameMonth = 3;
            _gameYear = 2022;
            _season = Season.Spring;
        }
        
        private void Update()
        {
            if (!_gameClockPause)
            {
                _tickTime += Time.deltaTime;

                if (_tickTime >= Settings.SecondThreshold)
                {
                    _tickTime -= Settings.SecondThreshold;
                    UpdateGameTime();
                }
            }

            if (Input.GetKey(KeyCode.T))
            {
                for (int i = 0; i < 60; i++)
                {
                    UpdateGameTime();
                }
            }
        }

        private void UpdateGameTime()
        {
            _gameSecond++;
            if (_gameSecond > Settings.SecondHold)
            {
                _gameMinute++;
                _gameSecond = 0;
                if (_gameMinute > Settings.MinuteHold)
                {
                    _gameHour++;
                    _gameMinute = 0;
                    if (_gameHour > Settings.HourHold)
                    {
                        _gameDay++;
                        _gameHour = 0;

                        if (_gameDay > Settings.DayHold)
                        {
                            _gameDay = 1;
                            _gameMonth++;

                            if (_gameMonth > 12)
                            {
                                _gameMonth = 1;
                            }

                            _mouthInSeason--;
                            if (_mouthInSeason == 0)
                            {
                                _mouthInSeason = 3;

                                int seasonNumber = (int)_season;
                                seasonNumber++;

                                if (seasonNumber > Settings.SeasonHold)
                                {
                                    seasonNumber = 0;
                                    _gameYear++;
                                }

                                _season = (Season)seasonNumber;

                                if (_gameYear > 9999)
                                {
                                    _gameYear = 2022;
                                }
                            }
                        }
                    }
                    EventHandler.CallGameDateEvent(
                        _gameHour,_gameDay,_gameMonth,_gameYear,_season);
                }

                EventHandler.CallGameMinuteEvent(_gameMinute,_gameHour);
            } 
            // Debug.Log($"Second: {_gameSecond}  Minute: {_gameMinute}");
        }
    }
}