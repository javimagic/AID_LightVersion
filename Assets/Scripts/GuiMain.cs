﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMain : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        Debug.Log(Input.GetAxis("Vertical"));
    }
}
