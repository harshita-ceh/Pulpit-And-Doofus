using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofusDataModel {

    public PlayerData player_data;
    public PulpitData pulpit_data;

    [System.Serializable]
    public class PlayerData {
        public float speed;
    }

    [System.Serializable]
    public class PulpitData {
        public float min_pulpit_destroy_time;
        public float max_pulpit_destroy_time;
        public float pulpit_spawn_time;
    }

    public static DoofusDataModel CreateFromJSON(string data) {
        return UnityEngine.JsonUtility.FromJson<DoofusDataModel>(data);
    }
}
