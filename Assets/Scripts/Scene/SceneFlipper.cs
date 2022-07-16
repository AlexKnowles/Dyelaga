using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;
using Dyelaga.GameManager;

namespace Dyelaga.Levels
{
    public class SceneFlipper : MonoBehaviour {  
        public void StartScene() {  
            SceneManager.LoadScene("Start");  
        }  
        public void GameScene() { 
            SceneManager.LoadScene("Main");  
        }  
        public void CompleteScene() {  
            SceneManager.LoadScene("Complete");  
        }
    }   
}