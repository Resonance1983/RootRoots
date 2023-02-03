using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	enum Game_Status
    {
        Ready,
        InGame,
        GameOver
    }
    private Game_Status status;
    private Game_Status Status {
        get
        { return status; }
        set
        { this.status = value;
            this.UpdateUI();
        }
    }
    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelOver;
    //public Player player;
    public int score;
    public Slider hpbar;

    void Start()
    {
        panelReady.SetActive(true);
        Status = Game_Status.Ready;

    }

    void Update()
    {
        
    }
    public void Player_OnDeath()
    {
        Status = Game_Status.GameOver;
    }

    public void StartGame()
    {
        Status = Game_Status.InGame;
    }

    public void UpdateUI()
    {
        this.panelReady.SetActive(this.Status == Game_Status.Ready);
        this.panelInGame.SetActive(this.Status == Game_Status.InGame);
        this.panelOver.SetActive(this.Status == Game_Status.GameOver);
    }

    public void Restart()
    {
        //player.Init();
        Status = Game_Status.Ready;

    }
}
