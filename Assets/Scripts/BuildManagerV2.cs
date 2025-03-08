using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManagerV2 : MonoBehaviour{
    #region Singleton
    public static BuildManagerV2 instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else{
            Destroy(this);
        } 
    }
    #endregion
    [SerializeField] private TurretBluePrint turretBlue;
    [SerializeField] private GameObject buildEffect; 
    private Transform positionJoueur;
    public bool canBuild { get { return turretBlue != null;}}


    void Start(){
        positionJoueur = GameManagerV2.instance.GetWaypointsManager().GetWaypointJoueur();
    }
    public void SetTourelle(GameObject turret){
        turretBlue.prefab = turret;
    }

    public void ConstruireTourelle(NodeConstructible node){
        if(PlayerStats.money < turretBlue.cost){
            Debug.Log("Pas assez d'argent pour ceci...");
            return;
        }
        PlayerStats.money -= turretBlue.cost;
        GameObject turret = (GameObject)Instantiate(turretBlue.prefab, node.GetPositionPourConstruire(), Quaternion.identity);
        node.SetTourelleNode(turret);
        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetPositionPourConstruire(), Quaternion.identity);
        Destroy(effect, 1f);
    }
}
