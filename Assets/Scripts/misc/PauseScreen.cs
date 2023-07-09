using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject title;


    [SerializeField] private Transition resumeButtonShow;
    [SerializeField] private Transition resumeButtonHide;
    [SerializeField] private Transition quitButtonShow;
    [SerializeField] private Transition quitButtonHide;
    [SerializeField] private Transition backgroundShow;
    [SerializeField] private Transition backgroundHide;
    [SerializeField] private Transition titleShow;
    [SerializeField] private Transition titleHide;
    [SerializeField] private Transition achievementShow;
    [SerializeField] private Transition achievementHide;
    
    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            AudioManager.Play("click2");
            Unpause();
        });
        
        quitButton.onClick.AddListener(() =>
        {
            AudioManager.Play("click2");
            Unpause();
            // todo: reload scene into title screen
            SceneManager.LoadScene("SampleScene");
        });
    }
    
    List<KeyValuePair<Transition, IEnumerator>> coroutines = new();

    private void Pause()
    {
        // rush all coroutines to end
        if (coroutines.Count > 0)
        {
            foreach (var c in coroutines)
            {
                c.Key.ForceEnd();
                while (c.Value.MoveNext())
                {
                }

                StopCoroutine(c.Value);
            }
        }

        coroutines.Clear();
        
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(resumeButtonShow, resumeButtonShow.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(quitButtonShow, quitButtonShow.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(backgroundShow, backgroundShow.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(titleShow, titleShow.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(achievementShow, achievementShow.Animate(null, true)));

        foreach(var c in coroutines)
            StartCoroutine(c.Value);

        Time.timeScale = 0;
    }

    private void Unpause()
    {
        Time.timeScale = 1;

        // rush all coroutines to end
        if (coroutines.Count > 0)
        {
            foreach (var c in coroutines)
            {
                c.Key.ForceEnd();
                while (c.Value.MoveNext())
                {
                }

                StopCoroutine(c.Value);
            }
        }
        
        coroutines.Clear();
        
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(resumeButtonHide, resumeButtonHide.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(quitButtonHide, quitButtonHide.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(backgroundHide, backgroundHide.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(titleHide, titleHide.Animate(null, true)));
        coroutines.Add( new KeyValuePair<Transition, IEnumerator>(achievementHide, achievementHide.Animate(null, true)));
        
        foreach(var c in coroutines)
            StartCoroutine(c.Value);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
}
