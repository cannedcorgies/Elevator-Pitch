using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public Animator doorRightAnim;
    public Animator doorLeftAnim;
    public Animator cameraAnim;
    public FadeTransition fader;

    public void ZoomIn () {
        StartCoroutine(ZoomAsynchronously());
    }

    IEnumerator ZoomAsynchronously () {
        cameraAnim.SetTrigger("zoom");
        yield return new WaitForSeconds(3);
        doorRightAnim.SetTrigger("open");
        doorLeftAnim.SetTrigger("open");
        yield return new WaitForSeconds(1);
        cameraAnim.SetTrigger("enter");
        yield return new WaitForSeconds(2);
        fader.FadeToLoader();
    }

}
