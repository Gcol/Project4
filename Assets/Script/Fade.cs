using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{

    [SerializeField] Image image;
    [SerializeField] Text txt;
    [SerializeField] float timerMaxIn = 1;
    [SerializeField] float timerMaxOut = 1;

    [SerializeField] float timerAfterMaxOut = 1;

    [SerializeField] string sceneName = "Main1";

    [SerializeField] bool isTextToFade = false;

    void Awake(){
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1 );
    }
    void Start(){
        StartCoroutine(FadeIn());
    }

    public void FadeOutF(){
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn(){
        StopCoroutine(FadeOut());
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1 );
        yield return new WaitForSeconds(0.5f);
        for(float i =1; i > 0; i-=Time.deltaTime/timerMaxIn){
            image.color = new Color(image.color.r, image.color.g, image.color.b, i );
             yield return new WaitForEndOfFrame();
        }

       yield return null;
    }

    IEnumerator FadeOut(){
        StopCoroutine(FadeIn());
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0 );
        for(float i =0; i < timerMaxOut; i+=Time.deltaTime){
            image.color = new Color(image.color.r, image.color.g, image.color.b, i );
             yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(timerAfterMaxOut/2);

        if(isTextToFade){

            for(float i =0; i < 1; i+=Time.deltaTime){
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, i );
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(3);

            for(float i =1; i > 0; i-=Time.deltaTime){
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, i );
                yield return new WaitForEndOfFrame();
            }

        }

        yield return new WaitForSeconds(timerAfterMaxOut/2);


        //if( AlreadyPlayed.Played){
        //      Application.Quit();
        //}
        //if (SceneManager.GetActiveScene().name == "Main1"){
        //    AlreadyPlayed.Played = true;
        //}
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
       yield return null;
    }
}
