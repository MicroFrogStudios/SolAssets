using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{

    public Sprite Heart, HeartEmpty;

    private PlayerMovement player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int indexC = 2;
    void OnLostLife()
    {
        if (indexC >= 0)
        {
            
            transform.GetChild(0).GetChild(indexC).gameObject.GetComponent<Image>().sprite = HeartEmpty;
            indexC--;
        }
       
    }
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (player != null) { 

        player.LostLife += OnLostLife;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
