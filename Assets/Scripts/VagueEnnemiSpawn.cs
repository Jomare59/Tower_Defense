using UnityEngine;
using System.Collections;
using TMPro;
public class VagueEnnemiSpawn: MonoBehaviour{
    [SerializeField] private Transform ennemiPrefab;
    [SerializeField] private Transform[] listeSpawnEnnemi;
    [SerializeField] private GameObject effetSpawnPrefab;
    [SerializeField] private AudioSource sound;

    [SerializeField] private Vector3 offset = new(0, -0.5f, 0);

    [SerializeField] private float IntervalleEnnemi = 0.5f;
    [SerializeField] private float timerEntreVague = 5.5f;
    [SerializeField] private float timer = 5f;
    private int vagueIndex = 0;
    public TextMeshProUGUI timerText;

    void Start(){
        listeSpawnEnnemi = GameManagerV2.instance.GetWaypointsManager().GetListeSpawnEnnemi();
    }
    private void Update() {
        if(timer <= 0f){
            StartCoroutine(SpawnVague());
            timer = timerEntreVague;
        }
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0f, Mathf.Infinity);
        timerText.text =  "Time : "+timer.ToString("F2");
    }

    private IEnumerator SpawnVague(){
        // vagueIndex++;
        vagueIndex = 1;
        sound.Play();
        int valeurAlea = Random.Range(0, listeSpawnEnnemi.Length-1);
        for(int i = 0; i < vagueIndex; i++){
            SpawnEnnemi(valeurAlea);
            yield return new WaitForSeconds(IntervalleEnnemi);
        }
    }

    private void SpawnEnnemi(int numWaypoint){
        Instantiate(ennemiPrefab, listeSpawnEnnemi[numWaypoint].position + offset , listeSpawnEnnemi[numWaypoint].rotation);
        SpawnEffet(listeSpawnEnnemi[numWaypoint].position, listeSpawnEnnemi[numWaypoint].rotation);
    }

    private void SpawnEffet(Vector3 position, Quaternion rotation){
        GameObject effet = Instantiate(effetSpawnPrefab, position, rotation);
        Destroy(effet, 1f);
    }
}
