using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    
    public static GameManager Instance
    {
        get => _instance ?? new GameObject("GameManager").AddComponent<GameManager>();
    }

    public static bool Exists() => _instance != null;

    #endregion

    #region private variable

    HashSet<BlockEntity> blocks = new HashSet<BlockEntity>();
    WaitForSeconds _second = new WaitForSeconds(1f);
    int _point = 0;
    int timer = 15;
    bool gameActive = false;

    #endregion

    public Action<int> OnPointChanged;
    public Action<int> OnTimerChanged;

    public int Points
    {
        get => _point;
        set {
            _point = Mathf.Max( value, 0 );
            OnPointChanged?.Invoke(_point);
        }
    }

    public bool PlayerWon => blocks.Count == 0 && timer > 0;

    void Awake()
    {
        if( _instance != null && _instance != this )
        {
            Destroy(this.gameObject);
            return;
        }
        else if ( _instance == null )
        {
            _instance = this;
        }

        gameActive = true;
        StartCoroutine(Countdown());
    }

    public void AddPoints(int point)
    {
        Points += point;
        timer += 2;
    }

    public void AddBlock(BlockEntity entity )
    {
        blocks.Add( entity );
    }

    public void RemoveBlock( BlockEntity entity )
    {
        blocks.Remove( entity );
        if( blocks.Count == 0 )
        {
            gameActive = false;
        }
    }

    IEnumerator Countdown()
    {
        while(gameActive)
        {
            yield return _second;
            if( --timer <= 0 )
            {
                // gameover!
                gameActive = false;
            }
            OnTimerChanged?.Invoke( timer );
        }
    }
}
