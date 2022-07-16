using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class ModalTester : MonoBehaviour
    {
        public string HeaderText, ContentText, OkLabel, CancleLabel, AltLabel;
        public Sprite Image;
        public ModalType type;
        public UnityEvent OnOkEvent, OnCancleEvent, OnAlternativeEvent;
        public void ShowModal()
        {
            UnityAction OnOk = null;
            UnityAction OnCancle = null;
            UnityAction OnAlternative = null;

            if (OnOkEvent.GetPersistentEventCount() > 0)
            {
                OnOk = OnOkEvent.Invoke;
            }

            if (OnCancleEvent.GetPersistentEventCount() > 0)
            {
                OnCancle = OnCancleEvent.Invoke;
            }
            if (OnAlternativeEvent.GetPersistentEventCount() > 0)
            {
                OnAlternative = OnAlternativeEvent.Invoke;
            }
            PopUpWindowManager.ShowPopUp(type, HeaderText, ContentText, OnOk, OnCancle, OnAlternative, Image, OkLabel, CancleLabel, AltLabel);
        }

        public void DebugLog(string message)
        {
            Debug.Log(message);
        }

    }
}