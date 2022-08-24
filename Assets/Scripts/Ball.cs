using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] private float releaseTime = .15f;
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private float maxDragDistance = 2f;
    [SerializeField] private GameObject nextBall;
    
    private Rigidbody2D _rigidbody;
    private SpringJoint2D _springJoint;
    private bool _isPressed = false;
    
        
    private void Start()
    {
        _springJoint = GetComponent<SpringJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Vector3.Distance(mousePosition, hook.position) > maxDragDistance)
                _rigidbody.position = hook.position + (mousePosition - hook.position).normalized * maxDragDistance;
            else
                _rigidbody.position = mousePosition;
        } 
    }


    private void OnMouseDown()
    {
        _isPressed = true;
        _rigidbody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        _rigidbody.isKinematic = false;

        StartCoroutine(Release());
    }

    
    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        _springJoint.enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2f);
        
        if(nextBall != null)
            nextBall.SetActive(true);
        else
        {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
