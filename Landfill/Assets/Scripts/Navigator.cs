using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    private bool loadingScene = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !loadingScene)
            
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
            Vector3 pos = Input.mousePosition;

            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));

            if (hitCollider) {
                Debug.Log (hitCollider.gameObject.name);
            }

            if (hit.collider != null)
            {
                print((hit.collider.gameObject.name));
                switch (hit.collider.gameObject.name)
                {

                    case "start":
                        loadingScene = true;
                        enabled = false;
                        //SceneManager.LoadScene("Ange");
                        StartCoroutine(LoadScene("Ange"));
                        StartCoroutine(LoadScene("Ange"));
                        break;
                    case "htp":
                        loadingScene = true;
                        enabled = false;
                        //SceneManager.LoadScene("howToPlay");
                        StartCoroutine(LoadScene("howToPlay"));
                        break;
                    default:
                        break;
                }

            }
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}

