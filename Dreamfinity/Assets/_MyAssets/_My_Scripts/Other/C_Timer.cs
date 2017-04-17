using UnityEngine;
using System.Collections;


namespace C_Utilities {

    public class C_Timer : MonoBehaviour
    {
        float time;
        float startTime;
        float timeScale;
        public C_Alarm alarm;

        public float StartTimer()
        {
            startTime = UnityEngine.Time.time;
            return startTime;
        }

        private void Update()
        {
            time = UnityEngine.Time.time - startTime;

            if (alarm.setTime >= time)
            {
                alarm.flag.Trip();
            }
        }

        public void ResetTimer()
        {
            startTime = 0;
            alarm.ResetAlarm();
            time = 0;

        }

        public float WatchTimer()
        {

            return time;
        }
    }
}
