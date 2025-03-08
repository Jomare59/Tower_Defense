using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeConstructible : MonoBehaviour{
    private Renderer rendererNode;
    private Color colorInitiale;
    private bool enConstruction = false;

    private GameObject tourelle;
    
    void Start(){
        rendererNode = GetComponent<Renderer>();
        if(rendererNode == null){
            Debug.Log("Renderer introuvé");
        }
        colorInitiale = rendererNode.material.color;
    }

    void OnMouseEnter(){
        // if(EventSystem.current.IsPointerOverGameObject()){
        //     return; //Permet de ne pas etre selectionné au dessus d'un UI.
        // }
        if(rendererNode && !tourelle){
            rendererNode.material.color = Color.cyan;
        }
    }

    void OnMouseExit(){
        if(rendererNode){
            rendererNode.material.color = colorInitiale;
        }
    }

    void OnMouseDown(){
        if(enConstruction){return;} 
        if(!BuildManagerV2.instance.canBuild){
            return;
        }
        if(tourelle){
            print("tourelle déjà présente !");
            return; 
        }
        
        enConstruction = true;
        BuildManagerV2.instance.ConstruireTourelle(this);
        Invoke("ReinitialiserClic", 0.1f);
    }

    private void ReinitialiserClic(){
        enConstruction = false;
    }
    public Vector3 GetPositionPourConstruire(){
        return transform.position + new Vector3(0, 0.4f, 0);
    }

    public void SetTourelleNode(GameObject tourelle){
        this.tourelle=tourelle;
    }

}
