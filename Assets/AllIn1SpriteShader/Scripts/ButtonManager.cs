using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject Fadein;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeIn()
    {
        Fadein.gameObject.SetActive(true);
    }

    public void SceneLoad(string name)
    {
        StartCoroutine(Late(name));
    }

    IEnumerator Late(string name)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(name);

    }

    
}
