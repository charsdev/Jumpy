using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Chars.Tools
{
    public class Timer : MonoBehaviour
    {
        public enum TimerState
        {
            Initial,
            Running,
            Paused,
            Finish
        }
        public enum TimerType
        {
            Seconds,
            MinutesSeconds,
            MinutesSecondsAndMilliseconds,
            HoursMinutesSecondsMilliSeconds,
            HoursMinutesSeconds,
            HoursMinutes
        }
        public enum TimerMode
        {
            Add,
            Substract
        }

        [SerializeField] private TextMeshProUGUI _timer;
        [SerializeField] private float _initialTime;
        [SerializeField] private float _currentTime;
        [SerializeField] private TimerType _currentType = TimerType.HoursMinutesSeconds;
        [SerializeField] private TimerState _currentState;
        [SerializeField] private TimerMode _currentMode;
        [SerializeField] private float _speed;

        public TimerState CurrentState => _currentState;

        private float Hours => Mathf.FloorToInt((_currentTime / 3600) % 24);
        private float Minutes => Mathf.FloorToInt((_currentTime / 60) % 60);
        private float Seconds => Mathf.FloorToInt(_currentTime % 60);
        private float MilliSeconds => (_currentTime % 1) * 1000;

        public UnityEvent OnFinish;

        private void Start()
        {
        }

        private void Update()
        {
            if (_currentState == TimerState.Running)
            {
                UpdateTimer();
                SetTimeText();
            }
        }

        public void StartTimer()
        {
            _currentTime = _initialTime;
            _currentState = TimerState.Running;
        }

        public void StopTimer() => SetTimerState(TimerState.Paused);

        public void SetInitialTime(float value) => _initialTime = value;

        private void SetTimeText() => _timer.text = GetTimeFormat(_currentTime);

        public void UpdateTimer()
        {
            if (_currentTime > 0)
            {
                switch (_currentMode)
                {
                    case TimerMode.Substract:
                        _currentTime -= Time.deltaTime * _speed;
                        break;
                    case TimerMode.Add:
                        _currentTime += Time.deltaTime * _speed;
                        break;
                }
            }
            else
            {
                _currentTime = 0;
                _currentState = TimerState.Finish;
                OnFinish.Invoke();
            }
        }

        public void SetTime(float newTime) => _currentTime = newTime;

        public void SetTimerType(TimerType type) => _currentType = type;

        public TimerType GetTimerType() { return _currentType; }

        public void SetTimerState(TimerState state) => _currentState = state;

        private string GetTimeFormat(float time)
        {
            string format = string.Empty;
            switch (_currentType)
            {
                case TimerType.Seconds:
                    format = $"{Seconds}";
                    break;
                case TimerType.MinutesSeconds:
                    format = $"{Minutes:00}:{Seconds:00}";
                    break;
                case TimerType.HoursMinutes:
                    format = $"{Hours:00}:{Minutes:00}";
                    break;
                case TimerType.HoursMinutesSeconds:
                    format = $"{Hours:00}:{Minutes:00}:{Seconds:00}";
                    break;
                case TimerType.MinutesSecondsAndMilliseconds:
                    format = $"{Minutes:00}:{Seconds:00}:{MilliSeconds:000}";
                    break;
                case TimerType.HoursMinutesSecondsMilliSeconds:
                    format = $"{Hours:00}:{Minutes:00}:{Seconds:00}:{MilliSeconds:000}";
                    break;
            }
            return format;
        }


    }
}