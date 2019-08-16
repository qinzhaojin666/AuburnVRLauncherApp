using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalAppLauncher : MonoBehaviour
{

    public GameObject networkManager;
    public GameObject worldCanvas;

    PhotonManager photonManager;
    Animator canvasAnim;

    // Start is called before the first frame update
    void Start()
    {
        photonManager = networkManager.GetComponent<PhotonManager>();
        canvasAnim = worldCanvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonManager.packageString != "default")
        {
            StartCoroutine(LaunchApp());
        }
    }

    private IEnumerator LaunchApp()
    {
        Debug.Log("Launching an external app!");
        canvasAnim.SetTrigger("Fade");

        yield return new WaitForSeconds(0.5f);

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", photonManager.finalString);

        jo.Call("startActivity", intent);
    }
}
