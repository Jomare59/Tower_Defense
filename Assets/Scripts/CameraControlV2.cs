using UnityEngine;

public class CameraControlV2 : MonoBehaviour{
    public Camera cameraPlayer;
    private bool playerUseCamera = true;
    private float largeurEcran;
    private float hauteurEcran;
    private float seuilBordure = 100f;
    private float vitesseRotation = 0.5f;

    void Start(){
        largeurEcran = Screen.width;
        hauteurEcran = Screen.height;
    }
    void Update(){
        MoveCameraAxe();
    }

    private void MoveCameraAxe(){
        if(!playerUseCamera){
            return;
        }
        if(Input.mousePosition.x < seuilBordure){
            Vector3 rotationCourante = cameraPlayer.transform.rotation.eulerAngles;
            rotationCourante.y -= Time.deltaTime + vitesseRotation;
            cameraPlayer.transform.rotation = Quaternion.Euler(rotationCourante);
        }
        if(Input.mousePosition.x > largeurEcran - seuilBordure){
            Vector3 rotationCourante = cameraPlayer.transform.rotation.eulerAngles;
            rotationCourante.y += Time.deltaTime + vitesseRotation;
            cameraPlayer.transform.rotation = Quaternion.Euler(rotationCourante);
        }
    }
}
