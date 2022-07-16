using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;
using Dyelaga.GameManager;

namespace Dyelaga.Levels
{
    public class SceneFlipper : MonoBehaviour {
        [SerializeField] public GameManager.Score Score;
        public void StartScene() {  
            SceneManager.LoadScene("Start");  
        }  
        public void GameScene() { 
            SceneManager.LoadScene("Main");  
        }  
        public void CompleteScene() {  
            SceneManager.LoadScene("Complete");  
        }
        public void ResetScore() {
            Score.CurrentScore = 0;
            Score.Died = false;
        }
    }   
}