using UnityEngine;

public class TourelleScript : MonoBehaviour{
    private Transform target;
    public float range = 15f;
    private float speedRotation = 5f;
    public string ennemiTag = "Ennemi";
    public Transform partiRotation;
    public float cadenceTire = 1f;
    private float couldownTire = 0f;
    public GameObject munitionPrefab;
    public Transform pointDeTir;

    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget(){
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag(ennemiTag);
        float distancePlusProche = Mathf.Infinity;
        GameObject EnnemiPlusProche = null;

        foreach(GameObject ennemi in ennemis) {
            float distanceToEnnemi = Vector3.Distance(transform.position, ennemi.transform.position);
            if(distanceToEnnemi < distancePlusProche){
                distancePlusProche = distanceToEnnemi;
                EnnemiPlusProche = ennemi;
            }
        }
        if(EnnemiPlusProche != null && distancePlusProche < range){
            target = EnnemiPlusProche.transform;
        }
        else{
            target = null;
        }
    }

    private void Update() {
        if(target == null){
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partiRotation.rotation, lookRotation, Time.deltaTime*speedRotation).eulerAngles;
        partiRotation.rotation = Quaternion.Euler(0f,rotation.y,0f);
        if(couldownTire <= 0f && Mathf.Abs(lookRotation.z-rotation.z) < 0.15f){
            Tirer();
            couldownTire = 1/cadenceTire;
        }
        couldownTire -= Time.deltaTime;
    }

    private void Tirer(){
        GameObject munitionEntite = Instantiate(munitionPrefab, pointDeTir.position, pointDeTir.rotation);
        Munition munition = munitionEntite.GetComponent<Munition>();
        if(munition != null){
            munition.SetTarget(target);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}