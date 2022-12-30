using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutScene : MonoBehaviour
{
    private bool fix = false;
    public Animator PlayerAnimator;
    public RuntimeAnimatorController PlayerAnmi;
    public PlayableDirector director;

    void OnEnable()
    {
        PlayerAnmi = PlayerAnimator.runtimeAnimatorController;
        PlayerAnimator.runtimeAnimatorController = null;
    }
    void Update()
    {
        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            PlayerAnimator.runtimeAnimatorController = PlayerAnmi;
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            director.time = 50f;
        }
    }
}
