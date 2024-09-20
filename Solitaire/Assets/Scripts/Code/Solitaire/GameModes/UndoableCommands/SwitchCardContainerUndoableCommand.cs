﻿/*
* Author:	Iris Bermudez
* Date:		19/09/2024
*/



using System.Threading.Tasks;
using Misc.Command;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Solitaire.GameModes.UndoableCommands {
    public class SwitchCardContainerUndoableCommand : IUndoableCommand {
        #region Variables
        private CardFacade card;
        private AbstractCardContainer originalCardContainer;
        private AbstractCardContainer newCardContainer;
        #endregion


        #region Constructor methods
        public SwitchCardContainerUndoableCommand( CardFacade _card, 
                                                AbstractCardContainer _originalCardContainer,
                                                AbstractCardContainer _newCardContainer ) {
            card = _card;
            originalCardContainer = _originalCardContainer;
            newCardContainer = _newCardContainer;
        }
        #endregion


        #region Public methods
        public async Task Execute() {
            // Recursively check childs
            var auxCardFacade = card;

            while( auxCardFacade != null ) {
                originalCardContainer.RemoveCard( auxCardFacade );
                newCardContainer.AddCard( auxCardFacade );
                auxCardFacade = auxCardFacade.ChildCard;

                await Task.Yield();
            }
        }

        public async Task Undo() {
            // Recursively check childs
            var auxCardFacade = card;

            while( auxCardFacade != null ) {
                newCardContainer.RemoveCard( auxCardFacade );
                originalCardContainer.AddCard( auxCardFacade );
                auxCardFacade = auxCardFacade.ChildCard;

                await Task.Yield();
            }
        }
        #endregion
    }
}