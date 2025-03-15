using System;
using UnityEngine;

public class EnnemiV2 : MonoBehaviour{
    private Transform cibleJoueur;
    private Vector3 ciblePosition;
    [SerializeField] private Animator animator;
    private bool dead = false;
    [SerializeField] private GameObject PrefabEffetMort;
    [SerializeField] private int life = 100;
    [SerializeField] private float vitesse = 1f;
    [SerializeField] private int valueMoney = 50;

    void Start(){
        RecupererJoueurPosition();
    }
    void Update(){
        if(!dead){
            Vector3 direction = ciblePosition - transform.position;
            if(direction.magnitude < 2f){
                TouchJoueur();
            }
            direction = direction.normalized;
            direction.y = 0; 
            transform.Translate(Time.deltaTime * vitesse * direction /10, Space.World);
            transform.LookAt(ciblePosition);
        }

    }
    private void RecupererJoueurPosition(){
        cibleJoueur = GameManagerV2.instance.GetWaypointsManager().GetWaypointJoueur();
        ciblePosition = cibleJoueur.position;
    }

    public void TakeDamage(int nbDamage){
        if(!dead){
            life -= nbDamage;
            if(life <= 0){
                PlayerStats.money += valueMoney;
                Mort();
            }
        }
    }

    private void Mort(){
        dead = true;
        EffetMort();
        animator.SetBool("Dead", true);
        Destroy(gameObject, 2f);    
    }
    private void EffetMort(){
        GameObject effet = Instantiate(PrefabEffetMort, transform.position, transform.rotation); //On instancie l'effet d'impact.
        Destroy(effet, 2f); //Et on le détruit 2secondes après.
    }

    private void TouchJoueur(){
        Mort();
        GameManagerV2.instance.TakeDamageJoueur();
    }

    public bool GetBoolDead(){
        return this.dead;
    }
}
