using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{

    private Animator mAnimator;
    public float idleTimeThreshold = 5f;
    
    private float timeSinceLastInput;
    // Start is called before the first frame update
    void Start()
    {
      mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      // Check for player input or activity here
      if (Input.anyKeyDown || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
      {
        mAnimator.SetBool("Stand", true);
        mAnimator.SetBool("Sit", false);
        mAnimator.SetBool("Lay", false);
        // Reset the timer if there is any input
        timeSinceLastInput = 0f;
      }
      else
      {
        // Increment the timer if there is no input
        timeSinceLastInput += Time.deltaTime;
            
        // Check if idle time threshold is reached
        if (timeSinceLastInput >= idleTimeThreshold)
        {
          // Set animator state to idle state
          mAnimator.SetBool("Sit", true);
        }
        if (timeSinceLastInput >= idleTimeThreshold * 2)
        {
          mAnimator.SetBool("Lay", true);
          // Set animator state to idle state
        }
      }
    }
}
