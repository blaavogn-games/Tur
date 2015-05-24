using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alarm : MonoBehaviour{
    public float time = 0;
    public AlarmListener listener;
    List<Timer> timers = new List<Timer>();

    public void setListener(AlarmListener listener) {
        this.listener = listener;
    }

    void Update() {
        time += Time.deltaTime;
        
        for (int i = timers.Count - 1; i >= 0; i-- ){
            Timer t = timers[i];
        
            if (t.targetTime <= time) {
                listener.onAlarm(t.type);
                if (t.repeat){
                    timers[i].targetTime = time + t.duration;
                } else {
                    timers.RemoveAt(i);
                }
            }
        }
    }

    public void addTimer(float duration, int type, bool repeat) {
        timers.Add(new Timer(time, duration, type, repeat));
    }
    
    class Timer {
        public float targetTime, duration;
        public int type;
        public bool repeat;

        public Timer(float time, float duration, int type, bool repeat) {
            this.duration = duration;
            this.targetTime = time + duration;
            this.type = type;
            this.repeat = repeat;
        }
    }
}
