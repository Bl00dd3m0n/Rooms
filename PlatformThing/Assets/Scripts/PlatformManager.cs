using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] Platforms;
    double timer;
    double fallingtimer;
    public GameObject Debris;
    public GameManager gm;
    Player player_script;
    // Start is called before the first frame update
    void Start()
    {
        player_script = gm.player.GetComponent<Player>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            player_script.Collectables.Add(new GameObject("Collectable 1"));//This is way too long
            Debug.Log("Fired");
            player_script.ResetPlayer();
            gm.LoadGamesLevel("Scene 2");
        }
    }
    private void FixedUpdate()
    {
        if (gm.GameInProgress)
        {
            fallingtimer += Time.deltaTime;
            timer += Time.deltaTime;
            if (fallingtimer > 4)
            {
                fallingtimer = 0;
                TriggerFall();
            }
            if (timer > 20)
            {
                gm.GameOver("You Won");
            }
        }
    }
    private void TriggerFall()
    {
        int safe = Random.Range(0, Platforms.Length);
        foreach(GameObject platform in Platforms)
        {
            if (platform != Platforms[safe])
            {
                Vector3 p = platform.transform.position;
                Instantiate(Debris, new Vector3(p.x,p.y+50,p.z),Quaternion.identity);
                
            }
        }
    }
}
