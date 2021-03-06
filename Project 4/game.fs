/// Card representations.
// An "enum"-type union for card suit.
type CardSuit = 
    | Spades 
    | Clubs
    | Diamonds
    | Hearts

// Kinds: 1 = Ace, 2 = Two, ..., 11 = Jack, 12 = Queen, 13 = King.
type Card = {suit : CardSuit; kind : int}


/// Game state records.
// One hand being played by the player: its cards, and a flag for whether it was doubled-down.
type PlayerHand = {
    cards: Card list; 
    doubled: bool
}

// All the hands being played by the player: the hands that are still being played (in the order the player must play them),
// and the hands that have been finished (stand or bust).
type PlayerState = {
    activeHands: PlayerHand list; 
    finishedHands: PlayerHand list
}

// The state of a single game of blackjack. Tracks the current deck, the player's hands, and the dealer's hand.
type GameState = {
    deck : Card list; 
    player : PlayerState; 
    dealer: Card list
}

// A log of results from many games of blackjack.
type GameLog = {playerWins : int; dealerWins : int; draws : int}

/// Miscellaneous enums.
// Identifies whether the player or dealer is making some action.
type HandOwner = 
    | Player 
    | Dealer

// The different actions a player can take.
type PlayerAction = 
    | Hit
    | Stand
    | DoubleDown
    | Split

// The result of one hand that was played.
type HandResult = 
    | Win
    | Lose
    | Draw


// This global value can be used as a source of random integers by writing
// "rand.Next(i)", where i is the upper bound (exclusive) of the random range.
let rand = new System.Random()


// UTILITY METHODS

// Returns a string describing a card.
let cardToString card =
    // Reminder: a 1 means "Ace", 11 means "Jack", 12 means "Queen", 13 means "King".
    // A "match" statement will be necessary. (The next function below is a hint.)
    let kind = match card.kind with
                |1 -> "Ace"
                |2 -> "Two" 
                |3 -> "Three"
                |4 -> "Four"
                |5 -> "Five" 
                |6 -> "Six" 
                |7 -> "Seven"
                |8 -> "Eight"
                |9 -> "Nine" 
                |10 -> "Ten"
                |11 -> "Jack"
                |12 -> "Queen"
                |13 -> "King"
                |_ -> "O___O"
    
    // "%A" can print any kind of object, and automatically converts a union (like CardSuit)
    // into a simple string.
    sprintf "%s of %A" kind card.suit


// Returns a string describing the cards in a hand.    
let handToString hand =
    // replace the following line with statement(s) to build a string describing the given hand.
    // The string consists of the results of cardToString when called on each Card in the hand (a Card list),
    // separated by commas. You need to build this string yourself; the built-in "toString" methods for lists
    // insert semicolons and square brackets that I do not want.
    hand |> List.map(fun a -> cardToString a) |> String.concat ", "

    // Hint: transform each card in the hand to its cardToString representation. Then read the documentation
    // on String.concat.

    
// Returns the "value" of a card in a poker hand, where all three "face" cards are worth 10
// and an Ace has a value of 11.
let cardValue card =
    match card.kind with
    | 1 -> 11
    | 11 | 12 | 13 -> 10  // This matches 11, 12, or 13.
    | n -> n
    
    // Reminder: the result of the match will be returned


// Calculates the total point value of the given hand (Card list). 
// Find the sum of the card values of each card in the hand. If that sum
// exceeds 21, and the hand has aces, then some of those aces turn from 
// a value of 11 to a value of 1, and a new total is computed.
// fill in the marked parts of this function.
let handTotal hand =
    //modify the next line to calculate the sum of the card values of each
    // card in the list. Hint: List.map and List.sum. (Or, if you're slick, List.sumBy)
    let sum = hand |> List.sumBy(fun s -> cardValue s)

    // modify the next line to count the number of aces in the hand.
    // Hint: List.filter and List.length. 
    //filter list by 11 "ace" value then count number of values of filtered list
    let numAces = hand |> List.filter(fun a -> a.kind = 1)|>List.length

    // Adjust the sum if it exceeds 21 and there are aces.
    if sum <= 21 then
        // No adjustment necessary.
        sum
    else 
        // Find the max number of aces to use as 1 point instead of 11.
        let maxAces = (float sum - 21.0) / 10.0 |> ceil |> int
        // Remove 10 points per ace, depending on how many are needed.
        sum - (10 * (min numAces maxAces))


// FUNCTIONS THAT CREATE OR UPDATE GAME STATES

// Creates a new, unshuffled deck of 52 cards.
// A function with no parameters is indicated by () in the parameter list. It is also invoked
// with () as the argument.
let makeDeck () =
    // Make a deck by calling this anonymous function 52 times, each time incrementing
    // the parameter 'i' by 1.
    // The Suit of a card is found by dividing i by 13, so the first 13 cards are Spades.
    // The Kind of a card is the modulo of (i+1) and 13. 
    List.init 52 (fun i -> let s = match i / 13 with
                                   | 0 -> Spades
                                   | 1 -> Clubs
                                   | 2 -> Diamonds
                                   | _ -> Hearts
                           {suit = s; kind = i % 13 + 1})


// Shuffles a list by converting it to an array, doing an in-place Fisher-Yates 
// shuffle, then converting back to a list.
// Don't worry about this.
let shuffleDeck deck =
    let arr = List.toArray deck

    let swap (a: _[]) x y =
        let tmp = a.[x]
        a.[x] <- a.[y]
        a.[y] <- tmp
    
    Array.iteri (fun i _ -> swap arr i (rand.Next(i, Array.length arr))) arr
    Array.toList arr


// Creates a new game state using the given deck, dealing 2 cards to the player and dealer.
let newGame (deck : Card list) =
    // Construct the starting hands for player and dealer.
    let playerCards = [deck.Head ; List.item 2 deck] // First and third cards.
    let dealerCards = [deck.Tail.Head ; List.item 3 deck] // Second and fourth.

    // Return a fresh game state.
    {deck = List.skip 4 deck;
    // the initial player has only one active hand.
     player = {activeHands = [{cards = playerCards; doubled = false}]; finishedHands = []}
     dealer = dealerCards}

//helper func that moves activehands to finishedhands
let finishedActive gameState =
    let playerState = gameState.player
    let active = playerState.activeHands.Head
    let newActive = playerState.activeHands.Tail
    let newfinHand = active :: playerState.finishedHands
    {gameState with player = {playerState with activeHands = newActive; finishedHands = newfinHand}}

// Given a current game state and an indication of which player is "hitting", deal one
// card from the deck and add it to the given person's hand. Return the new game state.
let hit handOwner gameState = 
    let topCard = List.head gameState.deck
    let newDeck = List.tail gameState.deck
    
    // Updating the dealer's hand is easy.
    if handOwner = Dealer then
        let newDealerHand = topCard :: gameState.dealer
        // Return a new game state with the updated deck and dealer hand.
        {gameState with deck = newDeck;
                        dealer = newDealerHand}
    else
        // updating the player is trickier. We are always working with the player's first
        // active hand. Create a new first hand by adding the top card to that hand's card list.
        // Then update the player's active hands so that the new first hand is head of the list; and the
        //     other (unchanged) active hands follow it.
        // Then construct the new game state with the updated deck and updated player.
        let playerState = gameState.player
        let activeHand = playerState.activeHands.Head
        let newPlayerHand = topCard :: activeHand.cards
        {gameState with deck = newDeck; player = {playerState with activeHands = [{cards = newPlayerHand; doubled = false}]}}


// Take the dealer's turn by repeatedly taking a single action, hit or stay, until 
// the dealer busts or stays.
let rec dealerTurn gameState =
    let dealer = gameState.dealer
    let score = handTotal dealer

    printfn "Dealer's hand: %s; %d points" (handToString dealer) score
    
    // Dealer rules: must hit if score < 17.
    if score > 21 then
        printfn "Dealer busts!"
        // The game state is unchanged because we did not hit. 
        // The dealer does not get to take another action.
        gameState
    elif score < 17 then
        printfn "Dealer hits"
        // The game state is changed; the result of "hit" is used to build the new state.
        // The dealer gets to take another action using the new state.
        gameState
        |> hit Dealer
        |> dealerTurn
    else
        // The game state is unchanged because we did not hit. 
        // The dealer does not get to take another action.
        printfn "Dealer must stay"
        gameState

//splits hand to 2 active hands; adds a card to each then returns to playerTurn        
let splitter gameState : GameState=
    let handOne = [gameState.player.activeHands.Head.cards.Head]
    let handtwo = []
    let oneHand = {cards = []; doubled = false}
    let newDeck = List.tail gameState.deck
    let secondHand = 0
    let newDeck = List.tail gameState.deck
    gameState

//takes current gameState from DoubledDown action and changes doubled to true
let doubled gameState : GameState =
    if gameState.player.finishedHands.IsEmpty then
        gameState
    else
        {gameState with player = {gameState.player with finishedHands = [{cards = gameState.player.finishedHands.Head.cards; doubled = true}]}}

// Take the player's turn by repeatedly taking a single action until they bust or stay.
let rec playerTurn (playerStrategy : GameState->PlayerAction) (gameState : GameState) =
    // TODO: code this method using dealerTurn as a guide. Follow the same standard
    // of printing output. This function must return the new game state after the player's
    // turn has finished, like dealerTurn.
    
    // Unlike the dealer, the player gets to make choices about whether they will hit or stay.
    // The "elif score < 17" code from dealerTurn is inappropriate; in its place, we will
    // allow a "strategy" to decide whether to hit. A "strategy" is a function that accepts
    // the current game state and returns true if the player should hit, and false otherwise.
    // playerTurn must call that function (the parameter playerStrategy) to decide whether
    // to hit or stay.
    let playerState = gameState.player

    if playerState.activeHands.IsEmpty then
        // A player with no active hands cannot take an action.
        gameState
    else
        // The next line is just so the code compiles. Remove it when you code the function.
        // print the player's first active hand. Call the strategy to get a PlayerAction.
        // Create a new game state based on that action. Recurse if the player can take another action 
        // after their chosen one, or return the game state if they cannot.
        let playerHand = playerState.activeHands.Head.cards
        let score = handTotal playerHand
        printfn "Player's hand: %s; %d points" (handToString playerHand) score
        let action = match playerStrategy gameState with
                        | Stand     -> finishedActive gameState
                        | DoubleDown-> hit Player gameState |> finishedActive |> doubled |> playerTurn playerStrategy 
                        | Hit       -> hit Player gameState |> playerTurn playerStrategy
                        | Split     -> playerTurn playerStrategy gameState //TODO: needs work
        action
                        
//checks who won dealer v player
let winLose dealer player =
    let dealerTotal = handTotal dealer
    let playerTotal = handTotal player
    match dealerTotal with
    | 21 -> match playerTotal with
            | 21 -> Draw
            | _  -> Lose
    | _ -> if dealerTotal = playerTotal then
                Draw
           elif dealerTotal > 21 && playerTotal <= 21 then //dealer bust
                Win
           elif playerTotal > 21 && dealerTotal <= 21 then //player bust
                Lose
           elif dealerTotal < playerTotal then
                Win
           else
                Lose

// Plays one game with the given player strategy. Returns a GameLog recording the winner of the game.
let oneGame playerStrategy gameState =
    // print the first card in the dealer's hand to the screen, because the Player can see
    // one card from the dealer's hand in order to make their decisions.
    gameState.dealer |> handToString |> printfn "Dealer is showing: %A"// fix this line

    printfn "Player's turn"
    // TODO: play the game! First the player gets their turn. The dealer then takes their turn,
    // using the state of the game after the player's turn finished.
    let state = playerTurn playerStrategy gameState
    printfn "\nDealer's turn"
    let dstate = dealerTurn state
    // TODO: determine the winner(s)! For each of the player's hands, determine if that hand is a 
    // win, loss, or draw. Accumulate (!!) the sum total of wins, losses, and draws, accounting for doubled-down
    // hands, which gets 2 wins, 2 losses, or 1 draw
    let outcome = winLose dstate.dealer dstate.player.finishedHands.Head.cards
    // The player wins a hand if they did not bust (score <= 21) AND EITHER:
    // - the dealer busts; or
    // - player's score > dealer's score
    // If neither side busts and they have the same score, the result is a draw.
    match outcome with
    | Lose -> if dstate.player.finishedHands.Head.doubled = true then
                {playerWins = 0; dealerWins = 2; draws = 0}
              else
                {playerWins = 0; dealerWins = 1; draws = 0}
    | Draw ->   {playerWins = 0; dealerWins = 0; draws = 1}
    | Win  -> if dstate.player.finishedHands.Head.doubled = true then
                {playerWins = 2; dealerWins = 0; draws = 0}
              else
                {playerWins = 1; dealerWins = 0; draws = 0}
    // this is a "blank" GameLog. Return something more appropriate for each of the outcomes
    // described above.


// Plays n games using the given playerStrategy, and returns the combined game log.
let manyGames n playerStrategy =
    // TODO: run oneGame with the playerStrategy n times, and accumulate the result. 
    // If you're slick, you won't do any recursion yourself. Instead read about List.init, 
    // and then consider List.reduce.

    //let endResult = List.init n (fun a -> oneGame playerStrategy) //need to figure out way to add GameLogs
    //                    |> List.reduce (fun a b -> match a with
    //                                              | _ -> {playerWins = 0; dealerWins = 0; draws = 0})
    // TODO: this is a "blank" GameLog. Return something more appropriate.
    //endResult
    {playerWins = 0; dealerWins = 0; draws = 0}
            

        
// PLAYER STRATEGIES
// Returns a list of legal player actions given their current hand.
let legalPlayerActions (playerHand : Card list) =
    let legalActions = [Hit; Stand; DoubleDown; Split]
    // One boolean entry for each action; True if the corresponding action can be taken at this time.
    let requirements = [
        handTotal playerHand < 21; 
        true; 
        playerHand.Length = 2;
        playerHand.Length = 2 && cardValue playerHand.Head = cardValue playerHand.Tail.Head
    ]

    List.zip legalActions requirements // zip the actions with the boolean results of whether they're legal
    |> List.filter (fun (_, req) -> req) // if req is true, the action can be taken
    |> List.map (fun (act, _) -> act) // return the actions whose req was true


// Get a nice printable string to describe an action.
let actionToString = function
    | Hit -> "(H)it"
    | Stand -> "(S)tand"
    | DoubleDown -> "(D)ouble down"
    | Split -> "S(p)lit"

// This strategy shows a list of actions to the user and then reads their choice from the keyboard.
let rec interactivePlayerStrategy gameState =
    let playerHand = gameState.player.activeHands.Head
    let legalActions = legalPlayerActions playerHand.cards

    legalActions
    |> List.map actionToString
    |> String.concat ", "
    |> printfn "What do you want to do? %s" 

    let answer = System.Console.ReadLine()
    // Return true if they entered "y", false otherwise.
    match answer.ToLower() with
    | "h" when List.contains Hit legalActions -> Hit
    | "s" -> Stand
    | "d" when List.contains DoubleDown legalActions -> DoubleDown
    | "p" when List.contains Split legalActions -> Split
    | _ -> printfn "Please choose one of the available options, dummy."
           interactivePlayerStrategy gameState

//this player always stands
let inactivePlayerStrategy gameState : PlayerAction = 
    Stand

//player always hits unless 21 or higher
let greedyPlayerStrategy gameState : PlayerAction=
    let total = gameState.player.activeHands.Head.cards |> handTotal
    if total < 21 then
        Hit
    else
        Stand //return state aka stand/do nothing

//player flips coin to decide hit.
//head = hit, other = tails
//Use System.Random from above: rand var
let coinFlipPlayerStrategy gameState : PlayerAction=
    if rand.Next(2) = 1 then //heads
        Hit
    else//tails stand
        Stand

//double down on two 5s, 11 total,
//total of 10 (unless dealers first card is 10 or 11 -> hit in this case
//total of 9 (unless dealers first card is a 2 7 or higher in which case hit
//Split when 2 cards of same kind except on 20 (stand)
//Otherwise if dealers first card is 2-6: stand if your total is 12 or greater. hit otherwise
//dealer's first card is 7-k: hit if your total is <= 16 stand other
//if dealer 1st card is Ace: hit if your total is 16 or less (if you have at least 1 Ace) otherwise hit if <= 11 otherwise stand
//IMPORTANT: if hand satisfies 1 or more conditions above always perform 1st action written. i.e. double down > split
let basicLogic playerHand dealerHand gameState : PlayerAction =
    let numFive = playerHand |> List.filter(fun a -> a.kind = 5)|>List.length
    let playerTotal = handTotal playerHand
    let dealerTotal = handTotal dealerHand
    let dealerFirst = dealerHand.Head
    let numAces = playerHand |> List.filter(fun a -> a.kind = 1)|>List.length
    //DoubleDown
    if playerTotal >= 21 then
        Stand
    elif numFive = 2 then
        DoubleDown
    elif playerTotal = 11 then DoubleDown
    elif playerTotal = 10 then
        if cardValue dealerFirst = 10 || cardValue dealerFirst = 11 then
            Hit
        else
            DoubleDown
    elif playerTotal = 9 then
        if cardValue dealerFirst = 2 || cardValue dealerFirst >= 7 then
            Hit
        else
            DoubleDown
    //Split
    elif cardValue playerHand.Head = cardValue playerHand.Tail.Head then
        if playerTotal = 20 then
            Stand
        else
            Split
    //dealer logic
    else
        if dealerFirst.kind >= 2 && dealerFirst.kind <= 6 then
            if playerTotal >= 12 then
                Stand
            else
                Hit
        elif dealerFirst.kind >= 7 then
            if playerTotal <= 16 then
                Hit
            else
                Stand
        elif dealerFirst.kind = 1 then
            if playerTotal <= 16 && numAces >= 1 then
                Hit
            elif playerTotal <= 11 then
                Hit
            else
                Stand
        else
            Hit
            

//takes GameState and returns Hit, DoubleDown, Stand or Split
let basicPlayerStrategy gameState : PlayerAction =
    let playerHand = gameState.player.activeHands.Head.cards
    let dealerHand = gameState.dealer
    let action = basicLogic playerHand dealerHand gameState
    action
        

[<EntryPoint>]
let main argv =
    // TODO: call manyGames to run 1000 games with a particular strategy.
    
    //TESTING STUFF BELOW
    let test = {suit = Hearts; kind = 1}
    let test2 = {suit = Clubs; kind = 1}
    let test3 = {suit = Clubs; kind = 11}
    //test3 |> cardToString |> printf "%A"
    //[test; test2; test3] |> handToString |> printf "%A"
    //[test; test2; test3] |> handTotal |> printf "%A"
    let deck = [{ suit = Hearts ; kind = 5}; { suit = Clubs ; kind = 13};{ suit = Spades ; kind = 5}; { suit = Hearts; kind = 13};{ suit = Clubs ; kind = 1}]
    //deck |> handToString |> printfn "%A"
    deck |> newGame |> oneGame basicPlayerStrategy |> printfn "%A"
    
    //ACTUAL RUN STUFF BELOW
    //makeDeck |> shuffleDeck |> newGame |> manyGames 1000 inactivePlayerStrategy |> printfn "%A"
    0 // return an integer exit code


