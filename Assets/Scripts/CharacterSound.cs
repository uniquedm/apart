using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    None,
    Grass,
    Wood,
    Metal,
    Rock
}

public class CharacterSound : MonoBehaviour
{
    AudioSource playerAudio;

    [Header("Sound FX")]
    public AudioClip jumpSound;

    public AudioClip[] footstepGrass;
    public AudioClip landSoundGrass;

    public AudioClip[] footstepWood;
    public AudioClip landSoundWood;

    public AudioClip[] footstepMetal;
    public AudioClip landSoundMetal;

    public AudioClip[] footstepRock;
    public AudioClip landSoundRock;

    [Header("Footsteps")]
    [SerializeField] float stepsLength = 1f;
    float stepsTimer = 0;

    [Header("Interactions")]
    public AudioClip pickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Footsteps(GroundType groundType, float speed)
    {
        if (groundType == GroundType.None)
            return;
        stepsTimer += Time.fixedDeltaTime * speed;

        if (stepsTimer >= stepsLength)
        {
            var step = footstepGrass;
            if (groundType == GroundType.Grass)
                step = footstepGrass;
            else if (groundType == GroundType.Wood)
                step = footstepWood;
            else if (groundType == GroundType.Metal)
                step = footstepMetal;
            else if (groundType == GroundType.Rock)
                step = footstepRock;
            AudioClip footstep = step[Random.Range(0, step.Length)];
            playerAudio.PlayOneShot(footstep);
            stepsTimer = 0;
        }
    }

    public void JumpSound()
    {
        playerAudio.PlayOneShot(jumpSound);
    }
    public void LandSound(GroundType groundType)
    {
        if (groundType == GroundType.Grass)
            playerAudio.PlayOneShot(landSoundGrass);
        else if (groundType == GroundType.Wood)
            playerAudio.PlayOneShot(landSoundWood);
        else if (groundType == GroundType.Metal)
            playerAudio.PlayOneShot(landSoundMetal);
        else if (groundType == GroundType.Rock)
            playerAudio.PlayOneShot(landSoundRock);
    }

    public void PickUpSound()
    {
        playerAudio.PlayOneShot(pickUpSound);
    }
}
