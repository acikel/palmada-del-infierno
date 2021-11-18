
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEditor;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public enum GameMusic
{
    Dialogue,
    Fight,
    Boss
}

public class AudioManager
{
    private static AudioManager instance;
    public static AudioManager Instance => instance ??= new AudioManager();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RuntimeInitialize()
    {
        instance = new AudioManager();
    }

    private AudioManager()
    {
        eventCache = new Dictionary<string, FMOD.GUID>();
        PreCacheEvents();

        musicInstance = RuntimeManager.CreateInstance(GetEvent(AudioEvent.Music));
    }

    private Dictionary<string, FMOD.GUID> eventCache;
    private EventInstance musicInstance;

    private const string ParamCombatBossCrossfade = "CombatBossCrossfade";
    private const string ParamStoryCombatCrossfade = "StoryCombatCrossfade";

    private FMOD.GUID CacheEvent(string audioEvent)
    {
        if (eventCache.ContainsKey(audioEvent))
            return eventCache[audioEvent];
        
        FMOD.GUID guid = FMODUnity.RuntimeManager.PathToGUID(audioEvent);
        eventCache.Add(audioEvent, guid);
        return guid;
    }

    private FMOD.GUID GetEvent(string audioEvent)
    {
        if (eventCache.ContainsKey(audioEvent))
            return eventCache[audioEvent];

        return CacheEvent(audioEvent);
    }

    public void PlayGameMusic()
    {
        musicInstance.getPlaybackState(out var playbackState);
        
        if (playbackState == PLAYBACK_STATE.STOPPED)
            musicInstance.start();
    }

    public void StopGameMusic()
    {
        musicInstance.getPlaybackState(out var playbackState);
        
        if (playbackState == PLAYBACK_STATE.PLAYING)
            musicInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public void ChangeGameMusic(GameMusic gameMusic)
    {
        switch (gameMusic)
        {
            case GameMusic.Fight:
                musicInstance.setParameterByName(ParamCombatBossCrossfade, 0f);
                musicInstance.setParameterByName(ParamStoryCombatCrossfade, .5f);
                break;
            case GameMusic.Boss:
                musicInstance.setParameterByName(ParamCombatBossCrossfade, 1f);
                musicInstance.setParameterByName(ParamStoryCombatCrossfade, 0f);
                break;
            case GameMusic.Dialogue:
                musicInstance.setParameterByName(ParamCombatBossCrossfade, 0f);
                musicInstance.setParameterByName(ParamStoryCombatCrossfade, 0f);
                break;
        }
    }

    public void PlayOneShot(string audioEvent, Vector3 position = default)
    {
        RuntimeManager.PlayOneShot(GetEvent(audioEvent), position);
    }
    
    public void PlayOneShot(EventReference eventReference, Vector3 position = default)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public void PlayOneShotAttached(string audioEvent, GameObject gameObject)
    {
        RuntimeManager.PlayOneShotAttached(GetEvent(audioEvent), gameObject);
    }

    public EventInstance CreateInstance(string audioEvent)
    {
        return RuntimeManager.CreateInstance(GetEvent(audioEvent));
    }

    private void PreCacheEvents()
    {
        CacheEvent(AudioEvent.Music);
        
        CacheEvent(AudioEvent.Bosses.Bobo);
        CacheEvent(AudioEvent.Bosses.Bucheli);
        CacheEvent(AudioEvent.Bosses.Shiva);
        
        CacheEvent(AudioEvent.Combat.Block);
        CacheEvent(AudioEvent.Combat.Impact);
        CacheEvent(AudioEvent.Combat.BlockImpact);
        CacheEvent(AudioEvent.Combat.BossAttackAngel);
        CacheEvent(AudioEvent.Combat.BossAttackBobo);
        CacheEvent(AudioEvent.Combat.BossAttackBucheli);
        CacheEvent(AudioEvent.Combat.BossAttackRene);
        CacheEvent(AudioEvent.Combat.BossStomp);
        CacheEvent(AudioEvent.Combat.EnemyAttack);
        CacheEvent(AudioEvent.Combat.EnemyDie);
        CacheEvent(AudioEvent.Combat.GruntFemale);
        CacheEvent(AudioEvent.Combat.GruntMale);
        CacheEvent(AudioEvent.Combat.PlayerAttack);
        CacheEvent(AudioEvent.Combat.PlayerDie);
        CacheEvent(AudioEvent.Combat.PlayerFootstep);
        
        CacheEvent(AudioEvent.UI.Select);
        CacheEvent(AudioEvent.UI.NegButton);
        CacheEvent(AudioEvent.UI.PosButton);
    }
}
