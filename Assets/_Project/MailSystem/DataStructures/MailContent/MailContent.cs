using UnityEngine;

namespace MailSystem
{
    [System.Serializable]
    public abstract class MailContent
    {
        [SerializeField] protected string content;
        protected abstract void GenerateContent();
        public override string ToString() => content;
    }
}