/* jshint -W098 */
/* global module */

/**
* Enum of actions a player can take when it is their turn
* @readonly
* @enum {string}
*/
var PlayerAction = {
    /** The player is requesting another card be dealt to them */
    Hit: 'Hit',
    /** The player would like to stick with their current hand */
    Stand: 'Stand'
};
Object.freeze(PlayerAction);

/**
* Enum of the card faces
* @readonly
* @enum {int} FaceValue
*/
var FaceValue = {
    Two: 2,
    Three: 3,
    Four: 4,
    Five: 5,
    Six: 6,
    Seven: 7,
    Eight: 8,
    Nine: 9,
    Ten: 10,
    Jack: 11,
    Queen: 12,
    King: 13,
    Ace: 14
};
Object.freeze(FaceValue);

/**
* Enum of the card suits
* @readonly
* @enum {string} Suits
*/
var Suit = {
    Diamonds: 'Diamonds',
    Spades: 'Spades',
    Clubs: 'Clubs',
    Hearts: 'Hearts'
};
Object.freeze(Suit);

/**
* @typedef {Object} Card
* @property {FaceValue} FaceVal - The face value of this card (ie Ace, Two,... King, etc)
* @property {Suit} Suit - The suit for this card.
*/

/**
* Describes a hand of cards in BlackJack
* @typedef {Object} BlackJackHand
* @property {number} HandTotal - The total value of the hand
* @property {Card[]} Cards - Array of the cards in this hand.
*/

/**
* The status of the player and dealer for this turn. Gives information about
* which cards the player is holding and their total value as determined by
* standard blackjack rules. Similar information about the dealer's hand is
* provided, however the face down card is omitted.
* @typedef {Object} TurnStatus
* @property {BlackJackHand} DealerHand - The hand for the dealer
* @property {BlackJackHand} PlayerHand - The hand for the current player
*/

/****************************************************************/
/****************************************************************/
/* helper functions                                             */
/****************************************************************/

/**
* Checks to see if a hand contains a specific card, optionally
* checking the suit as well as the face value.
* @param {BlackJackHand} hand - The hand to search
* @param {FaceVal} faceValue - The face value to search for
* @param {Suit} [suit] - The suit of the card
* @returns {boolean} - returns a value indicating whether a card with the
*   given face value is contained in the given hand.
*/
function containsCard(hand, faceValue, suit) {
    if (!hand || !hand.Cards) {
        console.warn('Hand has no cards!');
        return false;
    }

    var i;
    for (i = 0; i < hand.Cards.length; i++) {
        if (hand.Cards[i].FaceValue === faceValue) {
            if (!suit) {
                return true;
            }

            if (hand.Cards[i].Suit === suit) {
                return true;
            }
        }
    }
    return false;
}

/**
* Checks to see if a given hand contains at least one Ace
* @param {BlackJackHand} hand - the hand to search
* @returns {boolean} - returns a value indicating whether the hand contains an ace
*/
function containsAce(hand) {
    return containsCard(hand, FaceValue.Ace);
}

/****************************************************************/
/****************************************************************/
/* Start of custom bot code                                     */
/****************************************************************/

/**
* Creates a new BlackJackBot
* @class
*/
function BlackJackBot() {
}

/**
* Function that is called one time per bot to register the bot with the game.
* @param {Object} state - State tracking object. You can add any JSON serializable
*   properties to this object and they will be retained as this object is
*   passed in to each other method on your bot.
* @param {number} position - The seat number or position of this bot at the table.
* @returns {string} - The name to use to identify this bot.
*/
BlackJackBot.prototype.registerBot = function (state, position) {
    state.pos = position;

    return "{{BOTNAME}}";
};

/**
* Method that is called whenever a card is dealt to any player in the game.
* This includes the current player, players before and after this player and the dealer.
* @param {Object} state - State tracking object
* @param {number} position - The position at the table (order number) of the player being
*   dealt this card.
* @param {Card} card - The card that was dealt.
*/
BlackJackBot.prototype.cardDealt = function (state, position, card) {
    // TODO: Use to track state of cards dealt?
};

/**
* Function that is called for each player (in order of position) after the cards have been dealt.
* If this function returns PlayerAction.Hit and the player hasn't already busted, then a new card
* will be dealt to the player (the cardDealt event will fire) and this function will be called
* again with the updated {@link TurnStatus|TurnStatus} object..
* @param {object} state - The current state of the application
* @param {TurnStatus} turnStatus - Information about your hand and the dealer's face up card
* @returns {PlayerAction} - Whether the player would like another card (PlayerAction.Hit) or
*   would like to play out round with their current cards (PlayerAction.Stand)
*/
BlackJackBot.prototype.playTurn = function (state, turnStatus) {
    //var playerTotal = turnStatus.PlayerHand.HandTotal;
    //var dealerTotal = turnStatus.DealerHand.HandTotal;

    // won't know the actual dealer total at this point, only know
    // the value of the one card that is face up.

    return PlayerAction.Hit;
};

/**
 * Possible values for the result of a round.
 * @enum {int} PlayerResult 
 */
var PlayerResult = {
    Win: 0,
    Loss: 1,
    Push: 2,
    Bust: 3
};
Object.freeze(PlayerResult);

/**
* An object containing the results 
*   (you vs dealer only) for this round. This includes the win/loss/push 
*   status as well as the full hand for you and the dealer.
* @typedef {Object} RoundResults
* @property {PlayerResult} Result - The result of the round.
* @property {BlackJackHand} DealerHand - The hand for the dealer
* @property {BlackJackHand} PlayerHand - The hand for the current player
*/

/**
* Function that is called on each bot after a single round is complete.
* @param {object} state - The current state of the application
* @param {RoundResults} roundResults - an object containing the results 
*   (you vs dealer only) for this round. This includes the win/loss/push 
*   status as well as the full hand for you and the dealer.
*/
BlackJackBot.prototype.roundComplete = function (state, roundResults) {
    //TODO: Implement if desired
};

/**
 * Function that is called whenever a new shoe is shuffled.
 */
BlackJackBot.prototype.cardsShuffled = function() {
    //TODO: Implement if desired
};

/****************************************************************/
/****************************************************************/
/* End bot definition                                           */
/****************************************************************/