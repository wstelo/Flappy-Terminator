using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const KeyCode JumpButton = KeyCode.Space;
    private const KeyCode AttackButton = KeyCode.Mouse0;
    private const KeyCode PauseButton = KeyCode.Escape;

    private bool _isPaused = false;

    public event Action JumpButtonClicked;
    public event Action AttackButtonClicked;
    public event Action PauseButtonClicked;

    private void Update()
    {
        if (Input.GetKeyDown(JumpButton) && _isPaused == false)
        {
            JumpButtonClicked?.Invoke();
        }
        
        if (Input.GetKeyDown(AttackButton) && _isPaused == false)
        {
            AttackButtonClicked?.Invoke();
        }

        if(Input.GetKeyDown(PauseButton))
        {
            PauseButtonClicked?.Invoke();
        }
    }

    public void EnablePause()
    {
        _isPaused = true;
    }

    public void DisablePause()
    {
        _isPaused = false;
    }
}
