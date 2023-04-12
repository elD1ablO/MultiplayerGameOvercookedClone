using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoaderCallback : MonoBehaviour
{
    [SerializeField] private Image loadingProgress;

    private void Awake()
    {
        loadingProgress.fillAmount = 0;
    }

    private void Update()
    {
        Time.timeScale = 1f;
               
        loadingProgress.fillAmount += Time.deltaTime * 1f;        
        
        if (loadingProgress.fillAmount > 0.95f)
        {
            SceneLoader.LoadGameScene();
        }

    }
}
