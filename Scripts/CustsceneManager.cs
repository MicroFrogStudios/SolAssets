using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CustsceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GetComponent<VideoPlayer>().isPaused)
        {
            SceneManager.LoadScene("Testing");
        }
    }
}
