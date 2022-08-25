using UnityEngine;
using TMPro;
using UnityEngine.Localization;
namespace TimeSystem
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private TextMeshProUGUI timeStampText;
        [SerializeField] private TextMeshProUGUI seasonText;
        private int lastSpeedModifier = 1;

        private void OnEnable()
        {
            timeManager.RegisterForTimeUpdate(UpdateTimeStamp);
            timeManager.RegisterForTimeUpdate(UpdateSeason, TimeManager.SubscriptionType.Season);
            UpdateSeason();
            UpdateTimeStamp(timeManager.CurrentTimeStamp);
        }

        private void OnDestroy()
        {
            timeManager.UnregisterForTimeUpdate(UpdateTimeStamp);
            timeManager.RegisterForTimeUpdate(UpdateSeason, TimeManager.SubscriptionType.Season);
        }

        /// <summary>
        /// pauses or unpauses the time
        /// </summary>
        /// <param name="state">true = pausing, false = unpausing</param>
        public void SetPause(bool state)
        {
            if (state)
            {
                timeManager.PauseTime();
            }
            else
            {
                timeManager.ChangeSpeedModifier(lastSpeedModifier);
            }
        }

        /// <summary>
        /// Set the speed modificator
        /// </summary>
        /// <param name="amount"></param>
        public void SetTimeModificator(int amount)
        {
            lastSpeedModifier = amount;
            timeManager.ChangeSpeedModifier(amount);
        }

        private void UpdateTimeStamp(TimeStamp timeStamp) => timeStampText.text = timeStamp.ToString();

        private void UpdateSeason()
        {
            LocalizedString stringRef = new LocalizedString() { TableReference = "TimeManager", TableEntryReference = TimeManager.Now.Season.ToString().ToLower() };
            string seasonTranslation = stringRef.GetLocalizedString();
            seasonText.text = seasonTranslation;
        }
    }
}
