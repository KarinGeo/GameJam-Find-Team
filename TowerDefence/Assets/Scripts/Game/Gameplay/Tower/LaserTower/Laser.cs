using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Laser : DamageObject
{
    private Transform _lazerStart;
    private Transform _lazerEnd;
    private LineRenderer _lazer;
    private bool _isActive;

    public bool IsActive => _isActive;

    public void Awake()
    {

    }

    public void Init(Vector3 startPoint)
    {
        _lazer = transform.Find("Line").GetComponent<LineRenderer>();
        _lazerStart = transform.Find("StartVFX");
        _lazerEnd = transform.Find("EndVFX");
        _lazerStart.transform.position = startPoint;
        _lazer.SetPosition(0, new Vector3(startPoint.x, startPoint.y, -1));
        DeactivateLaser();
    }

    public void ActivateLaser()
    {
        _lazerStart.gameObject.SetActive(true);
        _lazerEnd.gameObject.SetActive(true);
        _lazer.enabled = true;
        _isActive = true;
    }

    public void DeactivateLaser()
    {
        _lazerStart.gameObject.SetActive(false);
        _lazerEnd.gameObject.SetActive(false);
        _lazer.enabled = false;
        _isActive = false;
    }

    public void OnActivate()
    {
        Vector3 end = _target.transform.position;
        _lazerEnd.transform.position = _target.transform.position;
        _lazer.SetPosition(1, new Vector3(end.x, end.y, -1));
    }

}
