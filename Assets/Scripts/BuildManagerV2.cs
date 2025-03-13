using UnityEditor;
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
    [SerializeField] private TurretBluePrint[] turretBlue;
    [SerializeField] private GameObject buildEffect; 
    private Transform positionJoueur;
    private int index = 0;
    public bool canBuild { get { return turretBlue != null;}}

    public void changeIndex(int index) {
        this.index = index;
    }
    void Start(){
        positionJoueur = GameManagerV2.instance.GetWaypointsManager().GetWaypointJoueur();
    }
    public void SetTourelle(GameObject turret){
        turretBlue[index].prefab = turret;
    }

    public void ConstruireTourelle(NodeConstructible node){
        if(PlayerStats.money < turretBlue[index].cost){
            Debug.Log("Pas assez d'argent pour ceci...");
            return;
        }
        PlayerStats.money -= turretBlue[index].cost;
        GameObject turret = (GameObject)Instantiate(turretBlue[index].prefab, node.GetPositionPourConstruire(), Quaternion.identity);
        node.SetTourelleNode(turret);
        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetPositionPourConstruire(), Quaternion.identity);
        Destroy(effect, 1f);
    }
}
