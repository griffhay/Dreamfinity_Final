using UnityEngine;

namespace C_Utilities
{
    public enum TriggerState { Untriggerd, EnterTrigger, Triggered, ExitTrigger }

    public struct Trigger
    {
        public TriggerState triggerState;
        public bool state;
        public bool prevState;

        public TriggerState TriggerStateInfo()
        {
            if (state == true)
            {
                if (prevState == false)
                {
                    return TriggerState.EnterTrigger;
                }

                if (prevState == true)
                {
                    return TriggerState.Triggered;
                }
            }

            if (state == false)
            {
                if (prevState == true)
                {
                    return TriggerState.ExitTrigger;
                }

                if (prevState == false)
                {
                    return TriggerState.Untriggerd;
                }
            }
            return TriggerState.Untriggerd;
        }

        void Update()
        {
            prevState = state;
        }

        public void Reset()
        {
            state = false;
        }

        public void Trip()
        {
            state = true;
        }

        public void Flip()
        {
            if (state)
            {
                state = false;
            }
            else
            {
                state = true;
            }
        }
    }
    
        
    
    

    public struct C_Alarm
    {

        public float setTime;
        public Trigger flag;

        public void SetTime(float _time)
        {
            setTime = _time;
        }

        public void ResetAlarm()
        {
            setTime = 0;
            flag.Reset();
        }


        void Update()
        {

        }
    }
}
