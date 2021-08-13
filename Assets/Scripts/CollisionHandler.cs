using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        crash = Resources.Load("Audios/crash") as AudioClip;
        success = Resources.Load("Audios/success") as AudioClip;
    }

    void Update() {
        ResponseToDebugKeys();
    }

    void ResponseToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }else if(Input.GetKey(KeyCode.C)){
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning || collisionDisable) return;
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
        crashParticle.Play();
        Invoke("ReloadLevel" ,1f);
        GetComponent<Movement>().enabled = false;
    }

     void startSuccessSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success); 
        successParticle.Play();
        Invoke("LoadNextLevel" ,1f);
        GetComponent<Movement>().enabled = false;
    }
}
