using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour{
    private Transform[] listeSpawnEnnemi;
    [SerializeField] private Transform spawnEnnemiParent;
    [SerializeField] private Transform waypointJoeur;
    void Awake(){
        RecupererSpawnsEnnemi();
    }

    private void RecupererSpawnsEnnemi(){
        listeSpawnEnnemi = new Transform[spawnEnnemiParent.childCount];
        for(int i = 0; i<listeSpawnEnnemi.Length; i++){
            listeSpawnEnnemi[i] = spawnEnnemiParent.GetChild(i);
        }
    }
    public Transform[] GetListeSpawnEnnemi(){
        return listeSpawnEnnemi;
    }

    public Transform GetWaypointJoueur(){
        return waypointJoeur;
    }
    

}
