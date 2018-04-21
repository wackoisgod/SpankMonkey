using Common.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum ApplicationState
    {
        Invalid,
        Splash,
        BuildingGame,
        Loading,
        MainMenu
    }

    public static GameManager Instance { get; private set; }
    public static DateTime StartupTime { get; private set; }

    public string LocalPlayerId = "aaaaaa";
    public string PlayerName = "HelloWorld";

    private ApplicationState _currentApplicationState;
    private ApplicationState _previousApplicationState;

    public ApplicationState CurrentApplicationState
    {
        get { return _currentApplicationState; }
        set
        {
            _previousApplicationState = _currentApplicationState;
            _currentApplicationState = value;

            OnApplicationStateChanged?.Invoke(_currentApplicationState, _previousApplicationState);
        }
    }

    public Action<ApplicationState, ApplicationState> OnApplicationStateChanged;

    private BaseManager[] _managers = new BaseManager[0];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _currentApplicationState = ApplicationState.Invalid;

            DontDestroyOnLoad(gameObject);

            // Add in the logger support for the UnityLogTarget
            LogManager.IsEnabled = true;
            LogManager.AttachLogTarget(new UnityLogTarget(Common.Logging.Logger.Level.Trace, Common.Logging.Logger.Level.Error, true));

            StartupTime = DateTime.Now;

            SetupManagers();
        }
    }

    private void SetupManagers()
    {
        List<BaseManager> mm = new List<BaseManager> {
            new UIManager()
        };

        _managers = mm.ToArray();
        foreach (BaseManager c in _managers)
        {
            c.Init();
        }
    }

    private bool _started;

    private void Start()
    {
        if (!_started)
        {
            _started = true;
            foreach (BaseManager c in _managers)
            {
                c.Begin();
            }
        }

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(.1f);

        Instance.CurrentApplicationState = ApplicationState.Splash;
    }

    private void Update()
    {
        float time = Time.time;
        float dt = Time.deltaTime;

        foreach (BaseManager c in _managers)
        {
            c.Update(time, dt);
        }
    }

    private void OnDestroy()
    {
        foreach (BaseManager c in _managers)
        {
            c.Destroy();
        }
    }
}
