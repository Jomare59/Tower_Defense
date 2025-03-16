using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerV2 : MonoBehaviour{
    #region Singleton
    public static GameManagerV2 instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else{
            Destroy(this);
        }
        
    }
    #endregion
    [SerializeField] private Animator animPlayer;
    [SerializeField] private AudioSource audioDamage;
    [SerializeField] private WaypointsManager waypointsManager;
    [SerializeField] private GameObject lightDamage;
    [SerializeField] private UI ui;
    public WaypointsManager GetWaypointsManager(){
        return waypointsManager;
    }
    void Update()
    {
       
    }

    public void TakeDamageJoueur(){
        PlayerStats.lives--;
        ChangeBoolTrembler();
        LightDamageActive();
        audioDamage.Play();
        ui.AnimeTextPV();
        Invoke("ChangeBoolTrembler", 0.2f);
        if(PlayerStats.lives <= 0){
            print("GameOver");
            Application.Quit();
        }

    }

    private void ChangeBoolTrembler(){
        animPlayer.SetBool("Trembler", !animPlayer.GetBool("Trembler"));
    }

    private void LightDamageActive(){
        lightDamage.SetActive(true);
        Invoke("LightDamageDisable", 0.2f);
    }

    private void LightDamageDisable(){
        lightDamage.SetActive(false);
    }
}
