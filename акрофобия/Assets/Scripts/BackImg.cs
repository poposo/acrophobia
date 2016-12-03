using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackImg : MonoBehaviour
{

    public Sprite[] sprArray = new Sprite[24];
    Image myImageComponent;
    float timer = 0.1f;
    float delay = 0.1f;
    int i = 0;


    void Start() {
        myImageComponent = GetComponent<Image>();   
    }

    void Update() {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            myImageComponent.sprite = sprArray[i];
            if (i == 20) {
                i = 0;
                timer = delay;
                return;
            }
            else {
                i++;
                timer = delay;
                return;
            }
        }
        
    }
}
    

