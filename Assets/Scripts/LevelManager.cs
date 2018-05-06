using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    

    Scene currentScene;
    string sceneName;

    private void Start()
    {

        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }


    public void LoadLevel(string name)
	{ 
        
        //Load level according to parameter
        Application.LoadLevel(name);
       

    }

	public void QuitRequest()
	{
		Application.Quit ();
	}

    public void LoadNextLevel()
    {
        
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    


}
