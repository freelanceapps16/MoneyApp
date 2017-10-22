using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanSelector : MonoBehaviour
{
    public string[] sceneNames;
	
	public void LoadScene(int sceneIndexInScenesList)
    {
        SceneManager.LoadScene(sceneNames[sceneIndexInScenesList], LoadSceneMode.Single);
    }
}
