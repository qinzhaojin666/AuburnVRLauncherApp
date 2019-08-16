using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteract : MonoBehaviour
{

    public Animator anim;
    public bool isWorking;
    public bool clickDown;
    public Animator camanim;
    public GameObject blackScreen;
    public string appID;
    public AudioSource hover;
    public AudioSource click;
    public GameObject selectSound;

    private GvrControllerInput gci;
    private bool startHover = true;
    private bool soundOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        camanim = blackScreen.GetComponent<Animator>();
        gci = GetComponent<GvrControllerInput>();
        hover = GetComponent<AudioSource>();
        click = selectSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        clickDown = GvrControllerInput.ClickButton;
        if(clickDown == true & isWorking == true & soundOnce == true)
        {
            soundOnce = false;
            click.Play(0);
            StartCoroutine("Fade");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pointer")
        {
            anim.SetBool("IsHover", true);
            isWorking = true;
            if(startHover == true)
            {
                hover.Play(0);
                startHover = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pointer")
        {
            anim.SetBool("IsHover", false);
            isWorking = false;
            startHover = true;
        }
    }

    IEnumerator Fade()
    {
        anim.SetTrigger("ReadyToLaunch");
        yield return new WaitForSeconds(.1f);
        camanim.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SwapApps();

    }

    private void SwapApps()
    {
        Debug.Log("App Swapping!!!");

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", appID);

        jo.Call("startActivity", intent);
    }
}
