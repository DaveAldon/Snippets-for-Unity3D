using UnityEngine;
using System.Collections;

public class doorAnim : MonoBehaviour {

  public float lengthOfAnim;
  public AnimationState anim;
  public string animName = "Open Door Up"; //Use whatever name your animation is called
  bool open = false; //Door state
  float animTime;

  public void Start() {
    //Basic assignment for easy maintenance and efficiency. If we can minimize passing strings all over, the better.
    anim = GetComponent<Animation>()[animName];
    lengthOfAnim = anim.length;
  }

  //Another class triggers this function, which sets the door state bool to the opposite of whatever it is
  public void triggerDoor() {
    open = !open;
    if(open) playOpen();
    else playClose();
  }

  public void playOpen() {
    //Play some sound
    GetComponent<AudioSource>().Play();
    //Make sure animation is playing forwards
    anim.speed = 1;
    GetComponent<Animation>().Play(animName);
    //Stop sound at end of animation
    StartCoroutine(StopSound());
  }

  public void playClose() {
    GetComponent<AudioSource>().Play();
    //Reverse animation
    anim.speed = -1;
    //If the animation is not at the beginning, then start wherever it was last
    if(anim.time > 0) anim.time = anim.time;
    //Or start the animation at the end
    else anim.time = lengthOfAnim;
    GetComponent<Animation>().Play(animName);
    StartCoroutine(StopSound());
  }

  IEnumerator StopSound() {
    //We're going to wait for the duration of the animation
    yield return new WaitForSeconds(lengthOfAnim);
    //Stop the audio dead in its tracks
    GetComponent<AudioSource>().Stop();
  }
}
