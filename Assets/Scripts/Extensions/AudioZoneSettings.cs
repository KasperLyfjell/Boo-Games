using UnityEditor;

[CustomEditor(typeof(DynamicAudioZone))]
public class AudioZoneSettings : Editor
{
    SerializedProperty PreDefinedSources;
    SerializedProperty RandomVolume;
    SerializedProperty RandomPitch;

    SerializedProperty minDelay;
    SerializedProperty maxDelay;

    SerializedProperty minVolume;
    SerializedProperty maxVolume;
    SerializedProperty minPitch;
    SerializedProperty maxPitch;

    SerializedProperty Source;
    SerializedProperty Clips;

    SerializedProperty AudioSources;


    private void OnEnable()
    {
        PreDefinedSources = serializedObject.FindProperty("PreDefinedSource");
        RandomVolume = serializedObject.FindProperty("RandomVolume");
        RandomPitch = serializedObject.FindProperty("RandomPitch");

        minDelay = serializedObject.FindProperty("minDelay");
        maxDelay = serializedObject.FindProperty("maxDelay");

        minVolume = serializedObject.FindProperty("minVolume");
        maxVolume = serializedObject.FindProperty("maxVolume");
        minPitch = serializedObject.FindProperty("minPitch");
        maxPitch = serializedObject.FindProperty("maxPitch");

        Source = serializedObject.FindProperty("Source");
        Clips = serializedObject.FindProperty("Clips");

        AudioSources = serializedObject.FindProperty("AudioSources");
    }

    public override void OnInspectorGUI()
    {
        DynamicAudioZone audioZone = (DynamicAudioZone)target;

        serializedObject.Update();


        EditorGUILayout.PropertyField(minDelay);
        EditorGUILayout.PropertyField(maxDelay);

        EditorGUILayout.PropertyField(PreDefinedSources);
        if (audioZone.PreDefinedSource)
        {
            EditorGUILayout.PropertyField(AudioSources);
        }
        else
        {
            EditorGUILayout.PropertyField(Source);
            EditorGUILayout.PropertyField(Clips);
        }

        EditorGUILayout.PropertyField(RandomVolume);
        if (audioZone.RandomVolume)
        {
            EditorGUILayout.PropertyField(minVolume);
            EditorGUILayout.PropertyField(maxVolume);
        }

        EditorGUILayout.PropertyField(RandomPitch);
        if (audioZone.RandomPitch)
        {
            EditorGUILayout.PropertyField(minPitch);
            EditorGUILayout.PropertyField(maxPitch);
        }


        serializedObject.ApplyModifiedProperties();
    }


}