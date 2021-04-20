using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    public delegate void GameStateCallback ();

    public event GameStateCallback OnGamePaused;
    public event GameStateCallback OnGamePlaying;

    private enum EGameState
    {
        Playing,
        Paused
    }

    private EGameState gameStatus;
    private EGameState GameStatus
    {
        get
        {
            return gameStatus;
        }

        set
        {
            switch (gameStatus)
            {

                case EGameState.Playing:
                    {
                        if (OnGamePlaying != null)
                            OnGamePlaying();
                    }
                    break;

                case EGameState.Paused:
                    {
                        if (OnGamePaused != null)
                            OnGamePaused();
                    }
                    break;

            }

            gameStatus = value;
        }
    }

    private bool newGame = true;

    // Start is called before the first frame update
    void Start()
    {
        GameStatus = EGameState.Paused;
        OnGamePaused();
    }

    // Update is called once per frame
    void Update()
    {
        if (newGame == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnGamePlaying();
                newGame = false;
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchGameState();
            }
        }
    }

    private void SwitchGameState()
    {
        switch (GameStatus)
        {

            case EGameState.Playing:
                {
                    GameStatus = EGameState.Paused;
                }
                break;

            case EGameState.Paused:
                {
                    GameStatus = EGameState.Playing;
                }
                break;

        }
    }
}
