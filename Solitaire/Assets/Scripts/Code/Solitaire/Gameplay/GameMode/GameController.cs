﻿/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using UnityEngine;
using UnityEngine.SceneManagement;
using Solitaire.Feedbacks;



namespace Solitaire.Gameplay.GameMode {
    public class GameController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private AbstractGameMode gameMode;
        [SerializeField]
        private DeckController deckController;
        [SerializeField]
        private FeedbacksGlue feedbacksGlue;
        #endregion


        #region MonoBehaviour methods
        private void Start() {
            StartGame();
        }


        private void Update() {
            #if UNITY_EDITOR
            if( Input.GetKeyUp( KeyCode.R) ) {
                SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
            }
            #endif
        }
        #endregion


        #region Public methods
        public void EndClearedGame( object _object, System.EventArgs _args ) {
            Debug.Log( "---------------------------" );
            Debug.Log( "Game cleared." );
            Debug.Log( "---------------------------" );
            feedbacksGlue.PlayGameOverFeedback();
        }
        #endregion


        #region Private methods
        private void StartGame() {
            deckController.onCardsCleared += EndClearedGame;
            gameMode.OnCardsCleared.AddListener( deckController.RemoveCardsFromGame );
            gameMode.Initialize( deckController.InitializeCards( gameMode.Suits,
                                                                gameMode.AmountOfEachSuit ) );
        }
        #endregion
    }
}