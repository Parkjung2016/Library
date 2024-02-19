#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EditorCoroutine
{
    public static EditorCoroutine StartCoroutine(IEnumerator _routine)
    {
        EditorCoroutine coroutine = new EditorCoroutine(_routine);
        coroutine.Start();
        return coroutine;
    }

    private readonly IEnumerator routine;
    private EditorCoroutine(IEnumerator _routine) => routine = _routine;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single)
            UnityEditor.EditorApplication.update -= Update;
    }

    private void Start()
    {
        EditorApplication.update += Update;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Stop()
    {
        EditorApplication.update -= Update;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!routine.MoveNext()) Stop();
    }
}
#endif