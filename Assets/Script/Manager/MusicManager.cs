using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance musicInstance;

    [SerializeField] private List<FMODUnity.EventReference> soundList;
    [SerializeField] private List<FMODUnity.EventReference> musicList;

    public static MusicManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SoundPlay(int id)
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundList[id]);
    }

    public void MusicClear()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void MusicPlay(int id)
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicList[id]);
        musicInstance.start();
        musicInstance.release();
    }
}
