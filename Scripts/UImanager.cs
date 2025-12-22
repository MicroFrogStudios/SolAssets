using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{

    public Sprite Heart, HeartEmpty;
    public GameObject gameOverUI, VictoryUI = null;
    private PlayerMovement player;
    private BossController sun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int indexC = 2;
    void OnLostLife()
    {
        if (indexC >= 0)
        {
            
            transform.GetChild(0).GetChild(indexC).gameObject.GetComponent<Image>().sprite = HeartEmpty;
            indexC--;
            if (indexC < 0)
            {
                if (!VictoryUI.activeInHierarchy)
                    gameOverUI.SetActive(true);
            }

        }
        
        

    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (player != null) {
            Debug.Log("Suscrito player");

            player.LostLife += OnLostLife;
        }
        GameObject Sun = GameObject.FindGameObjectWithTag("SolInvictus");
        
        Debug.Log(Sun.ToString());
        if (Sun != null)
        {
            Debug.Log("Suscrito sol");
            Sun.GetComponent<BossController>().deathTrigger += OnVictory;
            //sun.;
        }
    }

    private void OnVictory()
    {
        Debug.Log("ganamooos");
        gameOverUI.SetActive(false);
        VictoryUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
