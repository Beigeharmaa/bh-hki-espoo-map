using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soundbank : MonoBehaviour {

  public AudioClip sorryRepeat;
  public AudioClip sorryNoResponse;
  public AudioClip absolutelyyeah;
  public AudioClip thisismyjob;
  public AudioClip whathappensnow;
  public AudioClip whatisthismean;
  private List<AudioClip> repeats;
  private List<AudioClip> thanks;

	// Use this for initialization
	void Start () {
    repeats = new List<AudioClip>();

    repeats.Add(whathappensnow);
    repeats.Add(whatisthismean);

    thanks  = new List<AudioClip>();

    thanks.Add(absolutelyyeah);
    thanks.Add(thisismyjob);
  }
	
	// Update is called once per frame
	void Update () {
	
	}

  private void playSound (AudioClip newClip) {
    AudioSource audio = GetComponent<AudioSource>();         
    audio.clip = newClip;
    audio.Play();
  }

  public void repeat () {
    int pick = (int) Mathf.Round(Random.value);
    playSound(repeats[pick]);
  }

  public void noResponse () {
    playSound(sorryNoResponse);
  }

  public void thankYou () {
    int pick = (int) Mathf.Round(Random.value);
    playSound(thanks[pick]);
  }

}
