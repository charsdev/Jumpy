using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chars;
using Jumpy;

[RequireComponent(typeof(Follower))]
public class BrotherController : CharacterControllerBase
{
    private Follower _follower;
    private FollowersController _followersController;
    private bool _collected;
    [SerializeField] private CheckPoint _checkpoint;

    protected override void Start()
    {
        base.Start();
        _follower = GetComponent<Follower>();
        _followersController = GameManager.Instance.player.GetComponent<FollowersController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;

        if (other.CompareTag("Player") )
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (_collected) return;
        _checkpoint?.transform.SetParent(null);
        _followersController?.AddFollower(_follower);
        _collected = true;
    }
}
