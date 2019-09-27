using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public GameObject[] Doors;//Keeps a list of the Dorrs(This honestly may not be needed and will probably be removed)
    public Vector2[] Boundaries;//Boundaries(Will be used to make sure everything is properly deleted and things are kept within.
    public GameObject player;//Used to keep track of the player
    public bool GameInProgress;//Used to see if the game is still in progress
    public bool restart;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadGamesLevel(string levelname)
    {
        if (restart)
        {
            GameInProgress = true;
            restart = false;
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        else
        {
            DontDestroyOnLoad(player);
            SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
        }
    }

    //Gets in the cause of the game being over and prints it to the console.
    public void GameOver(string Text)
    {
        Debug.ClearDeveloperConsole();
        Debug.Log(Text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//to restart
        {
            restart = true;
            LoadGamesLevel("");
        }
    }
    void SaveLevel()
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fs = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open))
        {
            bf.Serialize(fs, player.GetComponent<Player>().pa);
        }
    }
    void LoadLevel()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            using (FileStream fs = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open))
            {
                player.GetComponent<Player>().pa = (PlayerAttributes)bf.Deserialize(fs);
            }
        }
    }
}
