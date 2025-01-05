using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameplayHUD : Sounds
{
    [DllImport("__Internal")]
    private static extern void RebirthExtern();

    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _pauseCanvas;
    [SerializeField] private Canvas _loseCanvas;
    [SerializeField] private Canvas _mobileCanvas;
    [SerializeField] private Button _adRestartButton;
    [SerializeField] private Points _points;
    [SerializeField] private Text _currentCount;
    [SerializeField] private Text _bestCount;
    [SerializeField] private GameObject _bgAudio;

    [SerializeField] private string rewardID; // ID для reward рекламы
    private Transform _lastCheckpoint;
    private int _pointsCount;
    private bool _isMobile;
    private bool _canTap;

    private void Awake()
    {
        _canTap = true;
    }

    public void Pause()
    {
        PlaySound(sounds[2]);
        Time.timeScale = 0;
        _mainCanvas.gameObject.SetActive(false);
        _pauseCanvas.gameObject.SetActive(true);
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        if (_canTap)
        {
            PlaySound(sounds[3]);
            Time.timeScale = 1;
            _mainCanvas.gameObject.SetActive(true);
            _pauseCanvas.gameObject.SetActive(false);
            if (_isMobile)
            {
                _mobileCanvas.gameObject.SetActive(true);
            }
            _points.StartCount();
        }
    }

    public void Restart()
    {
        if (_canTap)
        {
            PlaySound(sounds[0]);
            Time.timeScale = 1;
            int best = PlayerPrefs.GetInt("Best");
            _pointsCount = _points.PointsCount;
            if (_pointsCount > best)
            {
                PlayerPrefs.SetInt("Best", _pointsCount);
            }

            SceneManager.LoadScene("Gameplay");
        }
    }

    public void ToMenu()
    {
        if (_canTap)
        {
            Time.timeScale = 1;
            PlaySound(sounds[1]);
            int best = PlayerPrefs.GetInt("Best");
            _pointsCount = _points.PointsCount;
            if (_pointsCount > best)
            {
                PlayerPrefs.SetInt("Best", _pointsCount);
            }

            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Lose(Transform lastCheckpoint)
    {
        int best = PlayerPrefs.GetInt("Best");
        _pointsCount = _points.PointsCount;
        _currentCount.text = _pointsCount.ToString();
        if(_pointsCount > best)
        {
            _bestCount.text = _pointsCount.ToString();
            PlayerPrefs.SetInt("Best", _pointsCount);
        }
        else
        {
            _bestCount.text = best.ToString();
        }

        Time.timeScale = 0;
        YG2.InterstitialAdvShow(); // Вызов рекламы
        YG2.SetLeaderboard("Point", Points.liderboardPoints); // 
        _loseCanvas.gameObject.SetActive(true);
        _mainCanvas.gameObject.SetActive(false);
        _lastCheckpoint = lastCheckpoint;
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }
    }

    public void ContinueAd()
    {
        if (_canTap)
        {
            _bgAudio.SetActive(false);
            Time.timeScale = 0;
            _canTap = false;
            if (Application.isEditor)
            {
                Rebirth();
                SetTime();
            }
            else
            {
                RebirthExtern();
            }
        }
    }
    public void MyRewardAdvShow(string id)
    {
        YG2.RewardedAdvShow(rewardID, () =>
        {
            // Получение вознаграждения
            ContinueAd();
        });
    }
    public void Rebirth()
    {
        _loseCanvas.gameObject.SetActive(false);
        _adRestartButton.gameObject.SetActive(false);
        _mainCanvas.gameObject.SetActive(true);
        _levelManager.LastChance(_lastCheckpoint);
        if (_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(true);
        }
        _points.StartCount();
    }

    public void SetTime()
    {
        Time.timeScale = 1;
        _bgAudio.SetActive(true);
        _canTap = true;
    }

    private void SetPlatform(bool isMobile)
    {
        _isMobile = isMobile;
        if (!_isMobile)
        {
            _mobileCanvas.gameObject.SetActive(false);
        }
    }
}