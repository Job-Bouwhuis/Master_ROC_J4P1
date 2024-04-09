// Creator: Job
using ShadowUprising.UnityUtils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#nullable enable

namespace ShadowUprising.UI.Loading
{
    /// <summary>
    /// A singleton class providing a loading screen to switch between scenes in the game. Make sure that within the entire lifetime of the game there is only one instance of this class. if there are more, one of them will be destroyed.
    /// </summary>
    [DontDestroyOnLoad]
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        [Tooltip("The speed at which the loading screen will cover up the screen")]
        public float coverupSpeed = 5;

        /// <summary>
        /// All subscribers to this event should return the time in seconds they request for the loading screen to wait before starting the loading process.<br></br><br></br>
        /// 
        /// Can be used to animate out any UI elements that are currently on screen, or to play an animation before the loading screen starts.
        /// </summary>
        [HideInInspector] public MultipleReturnEvent<float> OnStartLoading { get; set; } = new();
        /// <summary>
        /// Invoked when the loading and preperation of a scene has been completed 
        /// </summary>
        [HideInInspector] public ClearableEvent<int> OnLoadingComplete { get; set; } = new();
        [SerializeField] private GameObject loadingScreenParent;
        [SerializeField] private LoadingSpinner spinner;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text tipText;
        [SerializeField] private ProgressBar sceneLoadBar;
        [SerializeField] private ProgressBar scenePrepBar;
        [SerializeField] private Vector3 hiddenPos;
        [SerializeField] private Vector3 shownPos;
        [SerializeField] private Vector3 targetPos;

        private AsyncOperation? sceneLoadOperation;
        private bool scenePrepComplete = false;
        private List<string> tips;

        /// <summary>
        /// Whether or not the loading screen is currently active.
        /// </summary>
        public bool IsLoading { get; private set; }
        /// <summary>
        /// The name of the scene that is currently loaded.
        /// </summary>
        public string CurrentScene => SceneManager.GetActiveScene().name;

        /// <summary>
        /// Shows the loading screen, but does not load a scene.
        /// </summary>
        public void Show()
        {
            IsLoading = true;
            targetPos = shownPos;
            text.text = "Loading...";
            spinner.StartSpinning();
        }
        /// <summary>
        /// Hides the loading screen.
        /// </summary>
        public void Hide()
        {
            targetPos = hiddenPos;
            spinner.StopSpinning();

            OnLoadingComplete.Invoke(0);

            IsLoading = false;
        }
        /// <summary>
        /// Initiates the loading of a scene, and shows the loading screen.<br></br>
        /// Once the scene is loaded and prepped, the loading screen will hide itself.
        /// </summary>
        /// <param name="sceneName"></param>
        public void ShowAndLoad(string sceneName)
        {
            StartCoroutine(WaitShowAndLoadScene(sceneName));
        }
        /// <summary>
        /// Loads the scene without showing the loading screen. still handles the <see cref="IScenePrepOperation"/> stuff.
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadWithoutShow(string sceneName)
        {
            StartCoroutine(WaitForSceneAnimations());
            SceneManager.LoadScene(sceneName);
            StartCoroutine(PrepScene());

        }

        private IEnumerator WaitForSceneAnimations()
        {
            IsLoading = true;
            var times = OnStartLoading.Invoke();
            Log.Push(times.Count + " subscribers to OnStartLoading");

            if (times.Count == 0)
                times.Add(0);

            float waitTime = times.Max();
            Log.Push("Waiting for " + waitTime + " seconds before starting loading process...");

            if (waitTime > 0)
                yield return new WaitForSecondsRealtime(waitTime);

            Log.Push("Clearing loadingscreen event subs");
            OnStartLoading.Clear();
            OnLoadingComplete.Clear();
            Log.Push($"Done > subs: {OnStartLoading.Subscribers}");
        }
        private IEnumerator WaitShowAndLoadScene(string sceneName)
        {
            yield return StartCoroutine(WaitForSceneAnimations());

            Show();

            scenePrepComplete = false;

            sceneLoadBar.progress = 0;
            scenePrepBar.progress = 0;
            sceneLoadBar.visualProgress = 0;
            scenePrepBar.visualProgress = 0;
            text.text = "Loading " + sceneName + "...";

            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadOperation.allowSceneActivation = false;

            StartCoroutine(WaitForSceneLoad());
            StartCoroutine(DoTips());
        }
        private IEnumerator WaitForSceneLoad()
        {
            while (sceneLoadOperation!.progress < .9f)
            {
                Log.Push("loading...");
                sceneLoadBar.progress = sceneLoadOperation.progress * 100;
                yield return new WaitForSecondsRealtime(1f);
            }
            Log.Push("Done");
            sceneLoadBar.progress = 1;

            StartCoroutine(PrepScene());
        }
        private IEnumerator DoTips()
        {
            Log.Push("Starting tips...");
            while (!scenePrepComplete)
            {
                tipText.text = "";
                string selectedTip = tips[Random.Range(0, tips.Count)];
                Log.Push("Selected tip: " + selectedTip);
                // animate tip text to appear on the text boxc
                foreach (char c in selectedTip)
                {
                    if (scenePrepComplete)
                        break;

                    tipText.text += c;
                    yield return new WaitForSecondsRealtime(.02f);
                }

                if (scenePrepComplete)
                    break;
                yield return new WaitForSecondsRealtime(2f);
            }
            Log.Push("Stopping Tips");
        }
        private IEnumerator PrepScene()
        {
            Log.Push("Prepping scene...");
            if(sceneLoadOperation is not null)
            {
                sceneLoadOperation.allowSceneActivation = true;
                sceneLoadOperation.allowSceneActivation = true;
                sceneLoadOperation = null;
            }

            yield return new WaitForSecondsRealtime(.5f);

            // find all objects that implement IScenePrepOperation
            var behaviors = FindObjectsOfType<MonoBehaviour>();

            var scenePrepOperations = behaviors.OfType<IScenePrepOperation>().ToList();
            if(scenePrepOperations.Count == 0)
            {
                scenePrepComplete = true;
                scenePrepBar.progress = 1;
                yield return new WaitForSecondsRealtime(1f);
                Log.Push("Done");
                Hide();
                yield break;
            }

            float contribution = 100f / scenePrepOperations.Count();

            foreach(var operation in scenePrepOperations)
                operation.IsComplete = false;
            foreach (var operation in scenePrepOperations)
                operation.StartPrep();

            int opIndex = 0;
            IScenePrepOperation currentOp = scenePrepOperations.FirstOrDefault();

            if (currentOp != null)
                Log.Push($"Starting {currentOp.GetType().Name}...");

            while (currentOp != null)
            {
                YieldInstruction? instruction = currentOp.PrepUpdate();

                if (instruction is Completed)
                {
                    currentOp.IsComplete = true;
                    instruction = null;
                }

                if (instruction != null)
                    yield return instruction;

                yield return new WaitForSecondsRealtime(.05f);

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

            yield return new WaitForSecondsRealtime(1f);
            Log.Push("Done");
            Hide();
        }

        private void Start()
        {
            string rawTips = Resources.Load<TextAsset>("Loading/Tips").text;

            tips = rawTips.Split('\n', System.StringSplitOptions.RemoveEmptyEntries).ToList();

            hiddenPos = Screen.height * 4 * Vector3.up;
            hiddenPos.x = transform.position.x;
            shownPos = transform.position;

            targetPos = hiddenPos;

            loadingScreenParent.transform.position = hiddenPos;
            loadingScreenParent.SetActive(true);
        }
        private void Update()
        {
            loadingScreenParent.transform.position = Vector3.Lerp(loadingScreenParent.transform.position, targetPos, Time.unscaledDeltaTime * coverupSpeed);

            if (sceneLoadOperation != null)
            {
                sceneLoadBar.progress = sceneLoadOperation.progress;
            }
        }
    }
}