// Creator: Job
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable enable

namespace ShadowUprising.UI.Loading
{
    [DontDestroyOnLoad]
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        public float coverupSpeed = 5;

        [SerializeField] private GameObject loadingScreenParent;
        [SerializeField] private LoadingSpinner spinner;
        [SerializeField] private TMP_Text text;
        [SerializeField] private LoadingBar sceneLoadBar;
        [SerializeField] private LoadingBar scenePrepBar;
        [SerializeField] private Vector3 hiddenPos;
        [SerializeField] private Vector3 shownPos;

        [SerializeField] private Vector3 targetPos;

        private AsyncOperation? sceneLoadOperation;
        bool scenePrepComplete = false;

        private void Start()
        {
            hiddenPos = Screen.height * 4 * Vector3.up;
            hiddenPos.x = transform.position.x;
            shownPos = transform.position;

            targetPos = hiddenPos;

            loadingScreenParent.transform.position = hiddenPos;
            loadingScreenParent.SetActive(true);
        }

        public void Show()
        {
            targetPos = shownPos;
            text.text = "Loading...";
            spinner.StartSpinning();
        }

        public void ShowAndLoad(string sceneName)
        {
            Show();
            sceneLoadBar.progress = 0;
            scenePrepBar.progress = 0;
            sceneLoadBar.visualProgress = 0;
            scenePrepBar.visualProgress = 0;
            text.text = "Loading " + sceneName + "...";

            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadOperation.allowSceneActivation = false;

            StartCoroutine(WaitForSceneLoad());
        }

        private IEnumerator WaitForSceneLoad()
        {
            while (sceneLoadOperation.progress < .9f)
            {
                Log.Push("loading...");
                sceneLoadBar.progress = sceneLoadOperation.progress * 100;
                yield return new WaitForSeconds(1f);
            }
            Log.Push("Done");
            sceneLoadBar.progress = 1;

            StartCoroutine(PrepScene());
        }

        private IEnumerator PrepScene()
        {
            Log.Push("Prepping scene...");
            sceneLoadOperation!.allowSceneActivation = true;
            sceneLoadOperation = null;

            yield return new WaitForSeconds(.5f);

            // find all objects that implement IScenePrepOperation
            var behaviors = FindObjectsOfType<MonoBehaviour>();

            var scenePrepOperations = behaviors.OfType<IScenePrepOperation>().ToList();

            float contribution = 100f / scenePrepOperations.Count();

            foreach (var operation in scenePrepOperations)
            {
                operation.StartPrep();
            }

            int opIndex = 0;
            IScenePrepOperation currentOp = scenePrepOperations.FirstOrDefault();

            if (currentOp != null)
                Log.Push($"Starting {currentOp.GetType().Name}...");

            while (currentOp != null)
            {
                YieldInstruction? instruction = currentOp.Update();

                if (instruction is Completed)
                {
                    currentOp.IsComplete = true;
                    instruction = null;
                }

                if (instruction != null)
                    yield return instruction;

                yield return new WaitForSeconds(.05f);

                if (currentOp.IsComplete)
                {
                    Log.Push("Completed " + currentOp.GetType().Name);
                    opIndex++;
                    currentOp = scenePrepOperations.ElementAtOrDefault(opIndex);
                    scenePrepBar.progress += contribution;

                    if (currentOp != null)
                    {
                        Log.Push($"Starting {currentOp.GetType().Name}...");
                    }
                }
            }

            scenePrepBar.progress = 1;
            scenePrepComplete = true;

            yield return new WaitForSeconds(1f);
            Log.Push("Done");
            Hide();
        }

        public void Hide()
        {
            targetPos = hiddenPos;
            spinner.StopSpinning();
        }

        private void Update()
        {
            loadingScreenParent.transform.position = Vector3.Lerp(loadingScreenParent.transform.position, targetPos, Time.deltaTime * coverupSpeed);

            if (sceneLoadOperation != null)
            {
                sceneLoadBar.progress = sceneLoadOperation.progress;
            }
        }
    }
}