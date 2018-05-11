using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace TrelloAPI
{
    public class BugReport : MonoBehaviour
    {
        [Header("Personal Trello Information")]
        public string yourKey = "Your Key";
        public string yourToken = "Your Token";
        public string currentBoard = "Your Trello Board";

        [Space(15)]
        [Tooltip("Places new uploaded cards on top of the list")]
        public bool newCardsOnTop = true;

        [Space(15)]
        [Header("Setup report types to appear in the dropdown here")]
        public List<Dropdown.OptionData> reportType;

        //Singleton instance
        public static BugReport instance;

        // Trello API obj
        private Trello trello;

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of this object
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator Start()
        {
            //Checks if we are already connected
            if (trello != null && trello.IsConnected())
            {
                Debug.Log("Connection with Trello server succesful");
                yield break;
            }

            // Creates our trello Obj with our key and token
            trello = new Trello(yourKey, yourToken);

            // gets the boards of the current user
            yield return trello.PopulateBoardsRoutine();
            trello.SetCurrentBoard(currentBoard);

            // gets the lists on the current board
            yield return trello.PopulateListsRoutine();

            // check if our reportType matches the lists in your trello board
            // otherwise it creates new lists and uploads them
            for (int i = 0; i < reportType.Count; i++)
            {
                if (!trello.IsListCached(reportType[i].text))
                {
                    var optionList = trello.NewList(reportType[i].text);
                    yield return trello.UploadListRoutine(optionList);
                }
            }

            // caches the new lists created (if any)
            yield return trello.PopulateListsRoutine();
        }

        public IEnumerator SendReportRoutine(TrelloCard card)
        {

            yield return trello.UploadCardRoutine(card);

            // Wait for one extra second to let the player read that his isssue is being processed
            yield return new WaitForSeconds(1);
        }

        // Sets gameObject active or inactive for timeInSeconds
        public IEnumerator SetActiveForSecondsRoutine(GameObject gameObject, float timeInSeconds, bool setActive = true)
        {
            gameObject.SetActive(setActive);
            yield return new WaitForSeconds(timeInSeconds);
            gameObject.SetActive(!setActive);
        }

        public Coroutine SendReport(string title, string description, string listName)
        {
            // if both the title and description are empty show error message to avoid spam
            if (title == "" || description == "")
            {
                return null;
            }

            TrelloCard card = trello.NewCard(title, description, listName);
            return StartCoroutine(SendReportRoutine(card));
        }
    }
}