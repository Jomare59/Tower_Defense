using System;
using UnityEngine;

public class Munition : MonoBehaviour{
    private Transform target; //Variable pour stocker l'entité cible.
    public GameObject impactEffet; //Variable pour stocker l'effet d'impact.
    public int damage = 50; //Le nombre de dégats que la munition inflige.
    [SerializeField]private float speed = 50f; //La vitesse de la munition.
    public float explosionRadius = 0; //Le rayon d'explosion de la muniton.
    

    public void Update(){
        if(target == null){
            Destroy(gameObject); //On détruit l'objet si la munition n'a pas de cible.
            return;
        }

        Vector3 dir = target.position - transform.position; //On récupère la direction dans laquelle la munition se dirigera.
        if(dir.magnitude <= speed * Time.deltaTime){ //Si inférieure ou égale à la distance parcourue en une frame c'est que l'on est assez proche pour toucher.
            TargetToucher();
            return; //On sort de la fonction.
        }
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //Si rien de tout ça on deplace la munition dans la direction.
        transform.LookAt(target); //Ainsi que son orientation.
    }

    private void TargetToucher(){
        EffetImpact();
        
        //Verif si c'est une bombe ou non.
        if(explosionRadius > 0){
            Explode(target);
        }
        else{
            Damage(target);
        }
        
        Destroy(gameObject);//On detruit la munition car elle vient de toucher.
        return;
    }

    private void EffetImpact(){
        GameObject effet = Instantiate(impactEffet, transform.position, transform.rotation); //On instancie l'effet d'impact.
        Destroy(effet, 5f); //Et on le détruit 5secondes après.
    }

    void Damage(Transform ennemi){
        EnnemiV2 e = ennemi.GetComponent<EnnemiV2>(); //On récupère le script Ennemi.
        if(e != null){
            e.TakeDamage(damage); //On appelle la fonction de l'ennemi damage.
        }
        else{
            Debug.Log("Error pas de script ennemi...");
        }
    }

    void Explode(Transform target){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); //Si c'est une explosion on crée une sphere invisible pour le joueur et on recupere tout les ennemis à l'interieur.
        foreach(Collider collider in colliders){
            if(collider.CompareTag("Ennemi")){
                Damage(collider.transform); //Si ils ont le nametag Ennemi on applique un damage.
            }
        }

    }
    

//--------------------------------------------Getter-Setter--------------------------------------------------------//

public void SetTarget(Transform _target){ //Setter pour la target.
        target = _target;
    }






//--------------------------------------------Debug----------------------------------------------------------------//
void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }














}
