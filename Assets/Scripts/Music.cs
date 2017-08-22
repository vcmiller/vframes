using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    public AudioClip clip;
    public float fadeRate = 0.5f;
    public float targetVolume = 1.0f;

    public AudioSource source { get; private set; }

    public static Music current { get; private set; }

    public void Awake() {
        source = GetComponent<AudioSource>();
        source.Stop();

        if (current == null) {
            Play();
        } else if (current != this) {
            if (current.source.clip == clip) {
                Destroy(gameObject);
            } else {
                source.volume = 0;
                current = null;

                Invoke("Play", 2.0f);
            }
        }
    }

    private void Play() {
        current = this;
        source.clip = clip;
        source.loop = true;
        source.Play();

        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void Update() {
        if (current == this) {
            source.volume = Mathf.MoveTowards(source.volume, targetVolume, fadeRate * Time.unscaledDeltaTime);
        } else {
            source.volume = Mathf.MoveTowards(source.volume, 0.0f, fadeRate * Time.unscaledDeltaTime);

            if (source.volume == 0) {
                Destroy(gameObject);
            }
        }
    }
}
