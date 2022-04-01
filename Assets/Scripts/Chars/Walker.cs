using UnityEngine;
using System.Collections;

public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Walker : MonoBehaviour
{
    [SerializeField] private Path _currentPath;
    [SerializeField] private Waypoint _currentWaypoint;
    public Node InitialNode;
    public float Speed;
    public bool FacingRight;
    [HideInInspector] public Waypoint LastWaypoint;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("LastWaypoint"))
        {
            string name = PlayerPrefs.GetString("LastWaypoint");
            GameObject lastWaypoint = GameObject.Find(name);
            if (lastWaypoint != null)
            {
                if (lastWaypoint.TryGetComponent(out Waypoint waypoint))
                {
                    InitialNode = waypoint;
                }
            }
        }
    }

    private void Start()
    {
        if (InitialNode == null) return;

        transform.position = InitialNode.position;

        if (InitialNode.TryGetComponent(out Waypoint waypoint))
        {
            _currentWaypoint = waypoint;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            MoveTo(Direction.RIGHT);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            MoveTo(Direction.LEFT);
        }
    }

    private void MoveTo(Direction direction)
    {
        if (_currentWaypoint != null)
        {
            _currentPath = _currentWaypoint.GetPath(direction);

            if (_currentPath != null && _currentPath.Direction == direction)
            {
                StopAllCoroutines();
                StartCoroutine(MoveAlongPath(_currentPath));
            }
        }
    }

    private IEnumerator MoveAlongPath(Path selectedPath)
    {
        var step = Speed * Time.deltaTime;
        var nodes = selectedPath.Nodes;
        var currentIndex = 0;
        var currentNode = nodes[0];
        var target = nodes[currentIndex];
        var end = nodes[nodes.Count - 1];
        _currentWaypoint = null;
        var initialPosition = transform.position;
        var diff = end.position.x - initialPosition.x;

        if (diff < 0 && FacingRight)
        {
            FacingRight = false;
            spriteRenderer.flipX = true;
        }
        else if (diff > 0 && !FacingRight)
        {
            FacingRight = true;
            spriteRenderer.flipX = false;
        }

        while (currentNode != end)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            else if (DebugTools.InBounds(currentIndex + 1, nodes))
            {
                currentNode = nodes[currentIndex];
                currentIndex++;
                target = nodes[currentIndex];
            }
            else
            {
                currentNode = nodes[currentIndex];
                _currentWaypoint = (Waypoint)currentNode;
            }

           yield return null;
        }
    }
}
