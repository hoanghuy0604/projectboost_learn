using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource audioSource;

    bool isTransitioning = false;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        crash = Resources.Load("Audios/crash") as AudioClip;
        success = Resources.Load("Audios/success") as AudioClip;
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning) return;
        switch(other.gameObject.tag){
            case "Friendly" : 
                Debug.Log("friendly");
                break;
            case "Fuel" : 
                Debug.Log("friendly");
                break;
            case "Finish" : 
                startSuccessSequence();
                break;
            default:
                startCrashSequence();
                break;
        }
    }
    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void startCrashSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel" ,1f);
        GetComponent<Movement>().enabled = false;
    }

     void startSuccessSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success); 
        Invoke("LoadNextLevel" ,1f);
        GetComponent<Movement>().enabled = false;
    }
}
