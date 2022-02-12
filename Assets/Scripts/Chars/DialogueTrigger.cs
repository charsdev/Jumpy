using UnityEngine;

namespace Chars
{
    public class DialogueTrigger : MonoBehaviour
    {
        private Collider2D _collider2D;
        [SerializeField] private Vector2 _offset;
        public GameObject PrefabDialogueBox;
        public GameObject OnZoneGameObject;
        private DialogueBox _dialogueBox;
        private bool _onZone;
        [Multiline]
        public string[] Senteces;
        public bool DialogEnabled = true;
        public KeyCode KeyCode;

        private void Start()
        {
            _collider2D = gameObject.GetComponent<Collider2D>();
            GameObject dialogueObject = GameObject.Instantiate(PrefabDialogueBox);
            _dialogueBox = dialogueObject.GetComponent<DialogueBox>();
            _dialogueBox.transform.position = new Vector2(_collider2D.bounds.center.x + _offset.x, _collider2D.bounds.max.y + _offset.y);
            _dialogueBox.transform.SetParent(gameObject.transform);
            _dialogueBox.Sentences = Senteces;
        }

        private void Update()
        {
            if (GameManager.Instance.GameIsPaused)
                return;
            
            if (Input.GetKeyDown(KeyCode) && _onZone && DialogEnabled)
            {
                OnZoneGameObject.SetActive(false);
                _dialogueBox.gameObject.SetActive(true);
            }

            if (!_onZone)
            {
                _dialogueBox.CloseDialogueBox();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onZone = true;
            OnZoneGameObject.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _onZone = false;
            OnZoneGameObject.SetActive(!DialogEnabled);
        }
    }


}