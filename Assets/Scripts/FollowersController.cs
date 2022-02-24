using System.Collections.Generic;
using UnityEngine;
using Chars;
using System.Linq;

public class FollowersController : MonoBehaviour
{
    [SerializeField] private List<Transform> _leaders = new List<Transform>();
    private int _steps = 10;

    private void Start()
    {
        _leaders.Add(transform);
    }

    private void Update()
    {
        
    }

    public void AddFollower(Follower follower)
    {
        follower.Setup(_leaders.Last(), _steps);
        _leaders.Add(follower.Transform);
    }

}
