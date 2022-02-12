using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Chars
{
    public class DialogueBox : MonoBehaviour
    {
        [SerializeField] public Text DialogueText;
        [Multiline]
        public string[] Sentences;
        public KeyCode Key;
        [SerializeField] private TypewriterEffect typewriterEffect;

        private IEnumerator StepThroughDialogue(string[] senteces)
        {
            for (int i = 0; i < senteces.Length; i++)
            {
                string dialogue = senteces[i];
                yield return RunTypingEffect(dialogue);
                DialogueText.text = dialogue;
                if (i == senteces.Length - 1)
                {
                    break;
                }
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(Key));
            }
            yield return new WaitUntil(() => Input.GetKeyDown(Key));
            CloseDialogueBox();
        }

        private IEnumerator RunTypingEffect(string dialogue)
        {
            typewriterEffect.Run(dialogue, DialogueText);
            while (typewriterEffect.IsRunning)
            {
                yield return null;
                if (Input.GetKeyDown(Key))
                {
                    typewriterEffect.Stop();
                }
            }
        }

        public void CloseDialogueBox()
        {
            gameObject.SetActive(false);
            DialogueText.text = string.Empty;
        }

        private void OnEnable()
        {
            StartCoroutine(StepThroughDialogue(Sentences));
        }
    }
}