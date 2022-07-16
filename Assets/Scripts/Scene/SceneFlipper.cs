using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;
using Dyelaga.GameManager;

namespace Dyelaga.Levels
{
    public class SceneFlipper : MonoBehaviour {  
        public void StartScene(bool restart = false) {  
            if (restart) {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>().CurrentScore = 0;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>().Died = false;
            }
            SceneManager.LoadScene("Start");  
        }  
        public void GameScene(bool restart = false) { 
            if (restart) {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>().CurrentScore = 0;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager.Score>().Died = false;
            }
            SceneManager.LoadScene("Main");  
        }  
        public void CompleteScene() {  
            SceneManager.LoadScene("Complete");  
        }
    }   
}